using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Maintenances;

namespace RelMa.Application.UseCases.Maintenances.V1.Commands;

public sealed class CreateMaintenanceCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<CreateMaintenanceCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreateMaintenanceCommand command, CancellationToken cancellationToken)
    {
        var entity = new MaintenanceEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            WorkOrderId = command.WorkOrderId,
            CronExpression = command.CronExpression,
            Images = command.Images,
        };

        unitOfWork.Repository<MaintenanceEntity, DefaultIdType>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
