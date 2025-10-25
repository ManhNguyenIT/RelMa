using Cortex.Mediator.Queries;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Maintenances.V1.Responses;
using RelMa.Application.UseCases.WorkOrders.V1.Responses;
using RelMa.Domain.Maintenances;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Maintenances.V1.Queries;

public sealed class GetMaintenanceQueryHandler(
    IUnitOfWork unitOfWork) : IQueryHandler<GetMaintenanceQuery, Shared.PagedResult<MaintenanceResponse>>
{
    public async Task<Shared.PagedResult<MaintenanceResponse>> Handle(GetMaintenanceQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<MaintenanceEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted)
            .Select(x => new MaintenanceResponse()
            {
                Id = x.Id,
                Images = x.Images,
                CronExpression = x.CronExpression,
                WorkOrderId = x.WorkOrderId,
                WorkOrder = x.WorkOrder == null ? null : new WorkOrderResponse()
                {
                    Id = x.WorkOrder.Id,
                    Title = x.WorkOrder.Title,
                },
                Assets = x.Assets == null ? null : x.Assets.Select(x => new Assets.V1.Responses.AssetResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    LocationId = x.LocationId,
                    Location = x.Location == null ? null : new Locations.V1.Responses.LocationResponse()
                    {
                        Id = x.Location.Id,
                        Name = x.Location.Name,
                    }
                }),
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
