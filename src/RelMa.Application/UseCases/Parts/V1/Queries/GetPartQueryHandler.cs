using Cortex.Mediator.Queries;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Parts.V1.Responses;
using RelMa.Domain.Parts;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Parts.V1.Queries;

public sealed class GetPartQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetPartQuery, Shared.PagedResult<PartResponse>>
{
    public async Task<Shared.PagedResult<PartResponse>> Handle(GetPartQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<PartEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted)
            .Select(x => new PartResponse()
            {
                Id = x.Id,
                LocationId = x.LocationId,
                Location = x.Location == null ? null : new Locations.V1.Responses.LocationResponse() { Id = x.Location.Id, Name = x.Location.Name, },
                MaterialId = x.MaterialId,
                Material = x.Material == null ? null : new Materials.V1.Responses.MaterialResponse() { Id = x.Material.Id, Name = x.Material.Name, Code = x.Material.Code, Description = x.Material.Description },
                Quantity = x.Quantity,
            });

        if (request.Includes?.Length > 0)
            query = query.Includes(request.Includes.Split(','));

        if (request.Filters?.Length > 0)
            query = query.ApplyFilters(request.Filters);

        query = request.Orders?.Length > 0
            ? query.ApplySorts(request.Orders)
            : query.OrderByDescending(o => o.Id);

        if (request.Columns?.Length > 0)
            query = query.Select(request.Columns.Split(','));

        return await query.ToPagedResultAsync(
            page: request.Page,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);
    }
}
