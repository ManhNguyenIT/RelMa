using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Maintenances;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Maintenances.V1.Commands;

public sealed class DeleteMaintenanceCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteMaintenanceCommand, bool>
{
    public async Task<bool> Handle(DeleteMaintenanceCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<MaintenanceEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Maintenance with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<MaintenanceEntity, DefaultIdType>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
