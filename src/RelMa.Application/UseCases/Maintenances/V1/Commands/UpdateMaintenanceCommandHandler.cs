using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Maintenances;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Maintenances.V1.Commands;

public sealed class UpdateMaintenanceCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateMaintenanceCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(UpdateMaintenanceCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<MaintenanceEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Maintenance with Id '{command.Id}' not found");

        entity.CronExpression = command.CronExpression;
        entity.Images = command.Images;
        entity.WorkOrderId = command.WorkOrderId;

        unitOfWork.Repository<MaintenanceEntity, DefaultIdType>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
