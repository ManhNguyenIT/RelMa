using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Quartz;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.Helpers;
using RelMa.Application.UseCases.Assets.V1.Responses;
using RelMa.Domain.Assets;
using RelMa.Domain.Locations;
using RelMa.Infrastructure.Extentions;
using RelMa.Shared.Exceptions;
using System.Data;
using System.Linq.Dynamic.Core;

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
        if (string.IsNullOrWhiteSpace(fileName))
            return;

        var tenantId = context.MergedJobDataMap.GetString("tenantId");

        var processed = 0;
        const int batchSize = 500;
        var status = new Dictionary<string, object>
        {
            ["status"] = ProcessingStatus.Processing,
            ["processed"] = processed,
            ["message"] = string.Empty
        };

        async Task UpdateStatusAsync()
        {
            await cache.SetAsync(
                key: "status",
                param: id,
                factory: _ => Task.FromResult(status),
                absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(10),
                cancellationToken: context.CancellationToken
            );
        }

        await UpdateStatusAsync();

        try
        {
            await unitOfWork.BeginTransactionAsync(context.CancellationToken);

            var filePath = Path.Combine(environment.ContentRootPath, "assets/uploads/assets", fileName);
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Không tìm thấy file: {filePath}");

            await using var fileStream = File.OpenRead(filePath);

            await foreach (var item in ExcelHelper.ImportExcelAsync<AssetResponse>(
                fileStream: fileStream,
                startRow: 2,
                sheetName: null,
                cancellationToken: context.CancellationToken))
            {
                var locationId = string.IsNullOrEmpty(item.LocationName)
                    ? (DefaultIdType?)null
                    : await unitOfWork.Repository<LocationEntity, DefaultIdType>()
                        .Find(x => !x.IsDeleted && x.Name == item.LocationName)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();

                unitOfWork.Repository<AssetEntity, DefaultIdType>().Add(new AssetEntity
                {
                    Id = DefaultIdType.CreateVersion7(),
                    Name = item.Name ?? throw new BadRequestException("Name cannot be null"),
                    Code = item.Code ?? throw new BadRequestException("Code cannot be null"),
                    Area = item.Area,
                    LocationId = locationId,
                    SerialNumber = item.SerialNumber,
                    Category = item.Category,
                    Description = item.Description,
                    Model = item.Model,
                    Status = Status.Open,
                    TenantId = tenantId
                });

                if (++processed % batchSize == 0)
                {
                    await unitOfWork.SaveChangesAsync(context.CancellationToken);

                    status["processed"] = processed;
                    await UpdateStatusAsync();
                }
            }

            if (processed % batchSize != 0)
                await unitOfWork.SaveChangesAsync(context.CancellationToken);

            await unitOfWork.CommitAsync(context.CancellationToken);

            status["status"] = ProcessingStatus.Completed;
            status["processed"] = processed;
            await UpdateStatusAsync();
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync(context.CancellationToken);
            status["status"] = ProcessingStatus.Failed;
            status["message"] = ex.Message;
            await UpdateStatusAsync();
        }
    }
}