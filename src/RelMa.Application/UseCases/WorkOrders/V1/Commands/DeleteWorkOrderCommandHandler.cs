using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.WorkOrders;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.WorkOrders.V1.Commands;

public class DeleteWorkOrderCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteWorkOrderCommand, bool>
{
    public async Task<bool> Handle(DeleteWorkOrderCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<WorkOrderEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"WorkOrder with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<WorkOrderEntity, DefaultIdType>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
