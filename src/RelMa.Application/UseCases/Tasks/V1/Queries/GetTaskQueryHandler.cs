using Cortex.Mediator.Queries;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Tasks.V1.Responses;
using RelMa.Domain.Tasks;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Tasks.V1.Queries;

public sealed class GetTaskQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetTaskQuery, PagedResult<TaskResponse>>
{
    public async Task<PagedResult<TaskResponse>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<TaskEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted, include: x => x.Include(i => i.Asset).ThenInclude(i => i!.Location))
            .Select(x => new TaskResponse()
            {
                Id = x.Id,
                Type = x.Type,
                Value = x.Value,
                AssetId = x.AssetId,
                Asset = x.Asset == null ? null : new Assets.V1.Responses.AssetResponse()
                {
                    Id = x.Asset.Id,
                    Name = x.Asset.Name,
                    LocationId = x.Asset.LocationId,
                    Location = x.Asset.Location == null ? null : new Locations.V1.Responses.LocationResponse()
                    {
                        Id = x.Asset.Location.Id,
                        Name = x.Asset.Location.Name,
                    }
                }
            });

        if (request.Includes?.Length > 0)
            query = query.Includes(request.Includes.Split(','));

        if (request.Filters?.Length > 0)
            query = query.Where(request.Filters);

        query = request.Orders?.Length > 0
            ? query.OrderBy(request.Orders)
            : query.OrderByDescending(o => o.Id);

        if (request.Columns?.Length > 0)
            query = query.Select(request.Columns.Split(','));

        return await query.ToPagedResultAsync(
            page: request.Page,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);
    }
}
