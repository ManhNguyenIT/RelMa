using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Tasks;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Tasks.V1.Commands;

public class UpdateTaskCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateTaskCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<TaskEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Task with Id '{command.Id}' not found");

        entity.AssetId = command.AssetId;
        entity.Type = command.Type;
        entity.Value = command.Value;

        unitOfWork.Repository<TaskEntity, DefaultIdType>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
