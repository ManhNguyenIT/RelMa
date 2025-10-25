using Cortex.Mediator.Queries;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Materials.V1.Responses;
using RelMa.Domain.Materials;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Materials.V1.Queries;

public sealed class GetMaterialQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetMaterialQuery, Shared.PagedResult<MaterialResponse>>
{
    public async Task<Shared.PagedResult<MaterialResponse>> Handle(GetMaterialQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<MaterialEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted)
            .Select(x => new MaterialResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Allocated = x.Allocated,
                Available = x.Available,
                Description = x.Description,
                Code = x.Code,
                Images = x.Images,
                Incoming = x.Incoming,
                Minimum = x.Minimum,
                OnHand = x.OnHand,
                Status = x.Status,
                Parts = x.Parts.Where(p => !p.IsDeleted).Select(p => new Parts.V1.Responses.PartResponse
                {
                    Id = p.Id,
                    MaterialId = p.MaterialId,
                    LocationId = p.LocationId,
                    Location = p.Location == null || p.Location.IsDeleted ? null : new Locations.V1.Responses.LocationResponse()
                    {
                        Id = p.Location.Id,
                        Name = p.Location.Name,
                    },
                    Quantity = p.Quantity,
                }).ToList()
            });

        if (request.Includes?.Length > 0)
            query = query.Includes(request.Includes.Split(','));

        if (request.Filters?.Length > 0)
            query = query.ApplyFilters(request.Filters);

        query = request.Orders?.Length > 0
            ? query.ApplySorts(request.Orders)
            : query.OrderByDescending(o => o.Name);

        if (request.Columns?.Length > 0)
            query = query.Select(request.Columns.Split(','));

        return await query.ToPagedResultAsync(
            page: request.Page,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);
    }
}
