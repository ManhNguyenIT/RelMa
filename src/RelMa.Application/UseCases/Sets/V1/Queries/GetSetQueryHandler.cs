using Cortex.Mediator.Queries;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Sets.V1.Responses;
using RelMa.Domain.Sets;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Sets.V1.Queries;

public sealed class GetSetQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSetQuery, PagedResult<SetResponse>>
{
    public async Task<PagedResult<SetResponse>> Handle(GetSetQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<SetEntity, DefaultIdType>()
            .Find(
                predicate: x => !x.IsDeleted,
                include: x => x.Include(i => i.Parts)
                                .ThenInclude(i => i.Location)
                                .Include(i => i.Parts)
                                .ThenInclude(i => i.Material))
                    .Select(x => new SetResponse()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Parts = x.Parts
                            .Select(p => new Parts.V1.Responses.PartResponse()
                            {
                                Id = p.Id,
                                Quantity = p.Quantity,
                                LocationId = p.LocationId,
                                MaterialId = p.MaterialId,
                                Location = p.Location == null || p.Location.IsDeleted ? null : new Locations.V1.Responses.LocationResponse()
                                {
                                    Id = p.Location.Id,
                                    Name = p.Location.Name
                                },
                                Material = p.Material == null || p.Material.IsDeleted ? null : new Materials.V1.Responses.MaterialResponse()
                                {
                                    Id = p.Material.Id,
                                    Name = p.Material.Name
                                }
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
