using Cortex.Mediator.Queries;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Checklists.V1.Responses;
using RelMa.Domain.Checklists;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Checklists.V1.Queries;

public sealed class GetChecklistQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetChecklistQuery, Shared.PagedResult<ChecklistResponse>>
{
    public async Task<Shared.PagedResult<ChecklistResponse>> Handle(GetChecklistQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<ChecklistEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted, include: x => x.Include(i => i.Tasks))
            .Select(x => new ChecklistResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Tasks = x.Tasks == null ? null : x.Tasks.Select(x => new Tasks.V1.Responses.TaskResponse()
                {
                    Id = x.Id,
                    Type = x.Type,
                    Value = x.Value,
                    AssetId = x.AssetId,
                    Asset = x.Asset == null ? null : new Assets.V1.Responses.AssetResponse()
                    {
                        Id = x.Asset.Id,
                        Area = x.Asset.Area,
                        Code = x.Asset.Code,
                        Category = x.Asset.Category,
                        Description = x.Asset.Description,
                        LocationId = x.Asset.LocationId,
                        Location = x.Asset.Location == null ? null : new Locations.V1.Responses.LocationResponse()
                        {
                            Id = x.Asset.Location.Id,
                            Name = x.Asset.Location.Name,
                            ParentId = x.Asset.Location.ParentId,
                        },
                        Model = x.Asset.Model,
                        Name = x.Asset.Name,
                        SerialNumber = x.Asset.SerialNumber
                    }
                }),
                WorkOrderId = x.WorkOrderId,
                WorkOrder = x.WorkOrder == null ? null : new WorkOrders.V1.Responses.WorkOrderResponse()
                {
                    Id = x.WorkOrder.Id,
                    Title = x.WorkOrder.Title,
                    Status = x.WorkOrder.Status
                }
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
