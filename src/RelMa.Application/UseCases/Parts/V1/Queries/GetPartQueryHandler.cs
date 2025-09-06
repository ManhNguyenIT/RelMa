using Cortex.Mediator.Queries;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Parts.V1.Responses;
using RelMa.Domain.Parts;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Parts.V1.Queries;

public sealed class GetPartQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetPartQuery, PagedResult<PartResponse>>
{
    public async Task<PagedResult<PartResponse>> Handle(GetPartQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<PartEntity, Ulid>()
            .Find(predicate: x => !x.IsDeleted, include: x => x.Include(i => i.Items).ThenInclude(i => i.Location).Include(i => i.Items).ThenInclude(i => i.Material))
            .Select(x => new PartResponse()
            {
                Id = x.Id,
                Name = x.Name,
                PartNumber = x.PartNumber,
                Category = x.Category,
                Cost = x.Cost,
                Description = x.Description,
                Image = x.Image,
                Quantity = x.Quantity,
                Items = x.Items.Select(i => new Items.V1.Responses.ItemResponse()
                {
                    Id = i.Id,
                    Quantity = i.Quantity,
                    LocationId = i.LocationId,
                    MaterialId = i.MaterialId,
                    Location = i.Location == null ? null : new Locations.V1.Responses.LocationResponse() { Id = i.Location.Id, Name = i.Location.Name },
                    Material = i.Material == null ? null : new Materials.V1.Responses.MaterialResponse() { Id = i.Material.Id, Name = i.Material.Name },
                })
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
