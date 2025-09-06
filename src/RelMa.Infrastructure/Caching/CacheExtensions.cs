using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using RelMa.Infrastructure.Converters;
using StackExchange.Redis;
using System.Text.Json;

namespace RelMa.Infrastructure.Caching;

public static class DistributedCacheExtensions
{
    private static readonly JsonSerializerOptions _options = new()
    {
        Converters = { new PagedResultJsonConverterFactory() }
    };

    public static async Task<T?> GetOrCreateAsync<T>(
        this IDistributedCache cache,
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan absoluteExpirationRelativeToNow,
        object? param = null,
        CancellationToken cancellationToken = default)
    {
        if (param is not null)
        {
            key = $"{key}-{JsonSerializer.Serialize(param)}";
        }
        var json = await cache.GetStringAsync(key, cancellationToken);
        if (!string.IsNullOrWhiteSpace(json))
        {
            return JsonSerializer.Deserialize<T>(json, _options);
        }

        var result = await factory(cancellationToken);

        if (result is not null)
        {
            await cache.SetStringAsync(
                key,
                JsonSerializer.Serialize(result, _options),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow
                },
                cancellationToken
            );
        }

        return result;
    }

    public static async Task RemoveCachesAsync(
    this IDistributedCache cache,
    IConnectionMultiplexer multiplexer,
    IOptions<RedisCacheOptions> options,
    string[] patterns,
    CancellationToken cancellationToken = default)
    {
        if (patterns == null || patterns.Length == 0 || options?.Value?.InstanceName == null)
            return;

        var database = multiplexer.GetDatabase();
        var endpoints = multiplexer.GetEndPoints();
        var instanceName = options.Value.InstanceName;

        var tasks = new List<Task>();
        foreach (var endpoint in endpoints)
        {
            var server = multiplexer.GetServer(endpoint);
            if (!server.IsConnected || server.IsReplica)
                continue;

            foreach (var pattern in patterns)
            {
                var fullPattern = $"{instanceName}{pattern}*";
                var batch = database.CreateBatch();
                var keys = new List<RedisKey>();

                await foreach (var key in server.KeysAsync(pattern: fullPattern).WithCancellation(cancellationToken))
                {
                    keys.Add(key);
                }

                if (keys.Count > 0)
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        await Task.WhenAll(keys.Select(k => cache.RemoveAsync(k.ToString(), cancellationToken)));

                        await batch.KeyDeleteAsync([.. keys], CommandFlags.FireAndForget);
                        batch.Execute();
                    }, cancellationToken));
                }
            }
        }

        await Task.WhenAll(tasks);
    }
}