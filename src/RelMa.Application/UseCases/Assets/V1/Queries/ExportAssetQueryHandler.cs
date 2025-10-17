using Cortex.Mediator.Queries;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.Helpers;
using RelMa.Application.UseCases.Assets.V1.Responses;
using RelMa.Domain.Assets;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Assets.V1.Queries;

public sealed class ExportAssetQueryHandler(
    IUnitOfWork unitOfWork,
    IWebHostEnvironment environment) : IQueryHandler<ExportAssetQuery, MemoryStream>
{
    public Task<MemoryStream> Handle(ExportAssetQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<AssetEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted, include: x => x.Include(i => i.Location))
            .Select(x => new AssetResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Area = x.Area,
                LocationId = x.LocationId,
                Code = x.SerialNumber,
                Category = x.Category,
                Description = x.Description,
                Model = x.Model,
                SerialNumber = x.Code,
                Location = x.Location == null ? null : new Locations.V1.Responses.LocationResponse() { Id = x.Location.Id, Name = x.Location.Name, },
            });

        if (request.Includes?.Length > 0)
            query = query.Includes(request.Includes.Split(','));

        if (request.Filters?.Length > 0)
            query = query.Where(request.Filters);

        query = request.Orders?.Length > 0
            ? query.OrderBy(request.Orders)
            : query.OrderByDescending(o => o.Name);

        if (request.Columns?.Length > 0)
            query = query.Select(request.Columns.Split(','));

        return ExcelHelper.ExportExcelStreamAsync(
            template: Path.Combine(environment.ContentRootPath, "assets", "templates", "asset.xlsx"),
            data: query.AsAsyncEnumerable(),
            startRow: 2,
            sheetIndex: 1,
            cancellationToken: cancellationToken);
    }
}
