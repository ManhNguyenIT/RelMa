using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Assets;
using RelMa.Domain.WorkOrders;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.WorkOrders.V1.Commands;

public class UpdateWorkOrderCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateWorkOrderCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(UpdateWorkOrderCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<WorkOrderEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"WorkOrder with Id '{command.Id}' not found");

        entity.Title = command.Title;
        entity.Description = command.Description;
        entity.Status = command.Status;
        entity.Category = command.Category;
        entity.Priority = command.Priority;
        entity.Images = command.Images;

        unitOfWork.Repository<WorkOrderEntity, DefaultIdType>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
