using Cortex.Mediator.Queries;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.WorkOrders.V1.Responses;
using RelMa.Domain.WorkOrders;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.WorkOrders.V1.Queries;

public sealed class GetWorkOrderQueryHandler(
    IUnitOfWork unitOfWork) : IQueryHandler<GetWorkOrderQuery, PagedResult<WorkOrderResponse>>
{
    public async Task<PagedResult<WorkOrderResponse>> Handle(GetWorkOrderQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<WorkOrderEntity, Ulid>()
            .Find(
                predicate: x => !x.IsDeleted,
                include: x => x.Include(i => i.Checklists)
                            .Include(i => i.Request)
                            .Include(i => i.Request).ThenInclude(i => i!.Asset).ThenInclude(i => i!.Location))
            .OrderByDescending(i => i.CreatedAt)
            .Select(x => new WorkOrderResponse()
            {
                Id = x.Id,
                Checklists = x.Checklists.Select(c => new Checklists.V1.Responses.ChecklistResponse()
                {
                    Id = c.Id,
                }),
                MaintenanceId = x.MaintenanceId,
                Maintenance = x.Maintenance == null ? null : new Maintenances.V1.Responses.MaintenanceResponse()
                {
                    Id = x.Maintenance.Id,
                },
                RequestId = x.RequestId,
                Request = x.Request == null ? null : new Requests.V1.Responses.RequestResponse()
                {
                    Id = x.Request.Id,
                    Priority = x.Request.Priority,
                    Status = x.Request.Status,
                    Title = x.Request.Title,
                    Description = x.Request.Description,
                    Image = x.Request.Image,
                    AssetId = x.Request.AssetId,
                    Asset = x.Request.Asset == null ? null : new Assets.V1.Responses.AssetResponse()
                    {
                        Id = x.Request.Asset.Id,
                        Name = x.Request.Asset.Name,
                        LocationId = x.Request.Asset.LocationId,
                        Location = x.Request.Asset.Location == null ? null : new Locations.V1.Responses.LocationResponse()
                        {
                            Id = x.Request.Asset.Location.Id,
                            Name = x.Request.Asset.Location.Name,
                        }
                    }
                },
                Status = x.Status,
            });

        if (request.Includes?.Length > 0)
            query = query.Includes(request.Includes.Split(','));

        if (request.Filters?.Length > 0)
            query = query.Where(request.Filters);

        if (request.Orders?.Length > 0)
            query = query.OrderBy(request.Orders);

        if (request.Columns?.Length > 0)
            query = query.Select(request.Columns.Split(','));

        return await query.ToPagedResultAsync(
            page: request.Page,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);
    }
}
