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
        params string[] patterns)
    {
        if (patterns == null || patterns.Length == 0)
            return;

        var endpoints = multiplexer.GetEndPoints();
        var tasks = endpoints.SelectMany(endpoint =>
        {
            var server = multiplexer.GetServer(endpoint);
            if (!server.IsConnected || server.IsReplica)
                return [];

            return patterns.Select(pattern => Task.Run(async () =>
            {
                await foreach (var key in server.KeysAsync(pattern: $"{options?.Value?.InstanceName}{pattern}*"))
                {
                    await cache.RemoveAsync(key.ToString());
                }
            }));
        });

        await Task.WhenAll(tasks);
    }
}