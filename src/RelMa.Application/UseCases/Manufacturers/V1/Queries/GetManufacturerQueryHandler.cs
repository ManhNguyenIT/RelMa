using Cortex.Mediator.Queries;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Manufacturers.V1.Responses;
using RelMa.Domain.Manufacturers;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Manufacturers.V1.Queries;

public sealed class GetManufacturerQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetManufacturerQuery, Shared.PagedResult<ManufacturerResponse>>
{
    public async Task<Shared.PagedResult<ManufacturerResponse>> Handle(GetManufacturerQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<ManufacturerEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted)
            .Select(x => new ManufacturerResponse()
            {
                Id = x.Id,
                Name = x.Name,
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
