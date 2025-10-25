using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Quartz;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Helpers;
using RelMa.Application.UseCases.Assets.V1.Responses;
using RelMa.Domain.Assets;
using RelMa.Infrastructure.Extentions;
using RelMa.Shared.Exceptions;

namespace RelMa.Infrastructure.Jobs;

public sealed class ImportAssetsJob(
    IUnitOfWork unitOfWork,
    IDistributedCache cache,
    IWebHostEnvironment environment) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        if (!DefaultIdType.TryParse(context.MergedJobDataMap.GetString("id"), out var id))
            return;

        var fileName = context.MergedJobDataMap.GetString("fileName");
        if (string.IsNullOrEmpty(fileName))
            return;
        var processed = 0;
        var status = new Dictionary<string, object>
        {
            ["Status"] = ProcessingStatus.Processing,
            ["Processed"] = processed,
            ["Message"] = string.Empty
        };

        await cache.SetAsync(
            key: $"status",
            param: id,
            factory: _ => Task.FromResult(status),
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10),
            cancellationToken: default
        );

#pragma warning disable CA1031 // Do not catch general exception types
        try
        {
            await unitOfWork.BeginTransactionAsync(default);
            using var fileStream = File.OpenRead(Path.Combine(environment.ContentRootPath, "assets/uploads/assets", fileName));
            var task = ExcelHelper.ImportExcelAsync<AssetResponse>(
                fileStream: fileStream,
                startRow: 2,
                sheetName: null,
                cancellationToken: default
            );

            await foreach (var item in task)
            {
                var entity = new AssetEntity()
                {
                    Id = DefaultIdType.CreateVersion7(),
                    Name = item.Name ?? throw new BadRequestException("Name cannot be null"),
                    Code = item.Code ?? throw new BadRequestException("Code cannot be null"),
                    Area = item.Area,
                    LocationId = item.LocationId,
                    SerialNumber = item.SerialNumber,
                    Category = item.Category,
                    Description = item.Description,
                    Model = item.Model,
                    Status = Status.Open,
                };

                unitOfWork.Repository<AssetEntity, DefaultIdType>().Add(entity);
                if (++processed % 100 == 0)
                    await unitOfWork.SaveChangesAsync(default);

                status["Processed"] = processed;
                await cache.SetAsync(
                    key: $"status",
                    param: id,
                    factory: _ => Task.FromResult(status),
                    absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10),
                    cancellationToken: default
                );
            }

            await unitOfWork.CommitAsync(default);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync(default);
            status["Status"] = ProcessingStatus.Failed;
            status["Message"] = ex.Message;
            await cache.GetOrCreateAsync(
                key: $"status",
                param: id,
                factory: _ => Task.FromResult(status),
                absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10),
                cancellationToken: default
            );
        }
#pragma warning restore CA1031 // Do not catch general exception types
    }
}
