using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Tasks;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Tasks.V1.Commands;

public class DeleteTaskCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteTaskCommand, bool>
{
    public async Task<bool> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<TaskEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Task with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<TaskEntity, DefaultIdType>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
