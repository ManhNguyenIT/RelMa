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
                Title = x.Title,
                Description = x.Description,
                AssetId = x.AssetId,
                Status = x.Status,
                Category = x.Category,
                Priority = x.Priority,
                Images = x.Images,
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
                },
                WorkOrderId = x.WorkOrderId,
                Files = x.Files == null ? null : x.Files.Where(f => !f.IsDeleted).Select(f => new Files.V1.Responses.FileResponse()
                {
                    Id = f.Id,
                    Ext = f.Ext,
                    Name = f.Name,
                    Size = f.Size,
                }),
            });

        if (!string.IsNullOrEmpty(request.Q))
            query = query.Where(x =>
                x.Title != null && EF.Functions.ILike(x.Title, $"%{request.Q}%")
                || x.Description != null && EF.Functions.ILike(x.Description, $"%{request.Q}%")
                || x.Asset != null && x.Asset.Name != null && EF.Functions.ILike(x.Asset.Name, $"%{request.Q}%")
                || x.Asset != null && x.Asset.Location != null && x.Asset.Location.Name != null && EF.Functions.ILike(x.Asset.Location.Name, $"%{request.Q}%")
            );

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
