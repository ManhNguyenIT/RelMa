using Cortex.Mediator.Queries;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Users.V1.Responses;
using RelMa.Application.UseCases.WorkOrders.V1.Responses;
using RelMa.Domain.WorkOrders;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.WorkOrders.V1.Queries;

public sealed class GetWorkOrderQueryHandler(
    IUnitOfWork unitOfWork) : IQueryHandler<GetWorkOrderQuery, Shared.PagedResult<WorkOrderResponse>>
{
    public async Task<Shared.PagedResult<WorkOrderResponse>> Handle(GetWorkOrderQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<WorkOrderEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted)
            .OrderByDescending(i => i.CreatedAt)
            .Select(x => new WorkOrderResponse()
            {
                Id = x.Id,
                No = x.No,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                Category = x.Category,
                Priority = x.Priority,
                AssigneeId = x.AssigneeId,
                Estimate = x.Estimate,
                Images = x.Images,
                Note = x.Note,
                Assignee = x.Assignee == null ? null : new UserResponse()
                {
                    Id = x.Assignee.Id,
                    Name = x.Assignee.Name,
                },
                RequestId = x.RequestId,
                Request = x.Request == null ? null : new Requests.V1.Responses.RequestResponse()
                {
                    Id = x.Request.Id,
                    Priority = x.Request.Priority,
                    Status = x.Request.Status,
                    Title = x.Request.Title,
                    Description = x.Request.Description,
                    Images = x.Request.Images,
                    AssetId = x.Request.AssetId,
                    Category = x.Request.Category,
                    WorkOrderId = x.Request.WorkOrderId,
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
                    },
                    Files = x.Request.Files == null ? null : x.Request.Files.Where(f => !f.IsDeleted).Select(f => new Files.V1.Responses.FileResponse()
                    {
                        Id = f.Id,
                        Ext = f.Ext,
                        Name = f.Name,
                        Size = f.Size,
                    }),
                },
                MaintenanceId = x.MaintenanceId,
                Maintenance = x.Maintenance == null ? null : new Maintenances.V1.Responses.MaintenanceResponse()
                {
                    Id = x.Maintenance.Id,
                    CronExpression = x.Maintenance.CronExpression,
                    WorkOrderId = x.Maintenance.WorkOrderId,
                },
                Checklists = x.Checklists.Where(c => !c.IsDeleted).Select(c => new Checklists.V1.Responses.ChecklistResponse()
                {
                    Id = c.Id,
                }),
            });

        if (!string.IsNullOrEmpty(request.Q))
            query = query.Where(x =>
                x.No != null && EF.Functions.ILike(x.No, $"%{request.Q}%")
                || x.Note != null && EF.Functions.ILike(x.Note, $"%{request.Q}%")
                || x.Title != null && EF.Functions.ILike(x.Title, $"%{request.Q}%")
                || x.Description != null && EF.Functions.ILike(x.Description, $"%{request.Q}%")
                || x.Request != null && x.Request.Title != null && EF.Functions.ILike(x.Request.Title, $"%{request.Q}%")
                || x.Assignee != null && x.Assignee.Name != null && EF.Functions.ILike(x.Assignee.Name, $"%{request.Q}%")
            );

        if (request.Includes?.Length > 0)
            query = query.Includes(request.Includes.Split(','));

        if (request.Filters?.Length > 0)
            query = query.ApplyFilters(request.Filters);

        query = request.Orders?.Length > 0
            ? query.ApplySorts(request.Orders)
            : query.OrderBy(x => x.No);

        if (request.Columns?.Length > 0)
            query = query.Select(request.Columns.Split(','));

        return await query.ToPagedResultAsync(
            page: request.Page,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);
    }
}
