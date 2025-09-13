using Cortex.Mediator.Queries;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Requests.V1.Responses;
using RelMa.Domain.Requests;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Requests.V1.Queries;

public sealed class GetRequestQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetRequestQuery, Shared.PagedResult<RequestResponse>>
{
    public async Task<Shared.PagedResult<RequestResponse>> Handle(GetRequestQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<RequestEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted, include: x => x.Include(i => i.Asset).ThenInclude(i => i!.Location))
            .Select(x => new RequestResponse()
            {
                Id = x.Id,
                Priority = x.Priority,
                Status = x.Status,
                Title = x.Title,
                Description = x.Description,
                Image = x.Image,
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
