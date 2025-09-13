using Cortex.Mediator.Queries;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Assets.V1.Responses;
using RelMa.Domain.Assets;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Assets.V1.Queries;

public sealed class GetAssetQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetAssetQuery, PagedResult<AssetResponse>>
{
    public async Task<PagedResult<AssetResponse>> Handle(GetAssetQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<AssetEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted, include: x => x.Include(i => i.Location).Include(i => i.Manufacturer))
            .Select(x => new AssetResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Area = x.Area,
                LocationId = x.LocationId,
                Barcode = x.Barcode,
                Category = x.Category,
                Description = x.Description,
                ManufacturerId = x.ManufacturerId,
                Model = x.Model,
                SerialNumber = x.SerialNumber,
                Location = x.Location == null ? null : new Locations.V1.Responses.LocationResponse() { Id = x.Location.Id, Name = x.Location.Name, },
                Manufacturer = x.Manufacturer == null ? null : new Manufacturers.V1.Responses.ManufacturerResponse() { Id = x.Manufacturer.Id, Name = x.Manufacturer.Name, },
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

        return await query.ToPagedResultAsync(
            page: request.Page,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);
    }
}
