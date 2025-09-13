using Cortex.Mediator.Queries;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Storages.V1.Responses;
using RelMa.Domain.Storages;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Storages.V1.Queries;

public sealed class GetStorageQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetStorageQuery, PagedResult<StorageResponse>>
{
    public async Task<PagedResult<StorageResponse>> Handle(GetStorageQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<StorageEntity, DefaultIdType>()
            .Find(predicate: x => !x.IsDeleted)
            .Select(x => new StorageResponse()
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
