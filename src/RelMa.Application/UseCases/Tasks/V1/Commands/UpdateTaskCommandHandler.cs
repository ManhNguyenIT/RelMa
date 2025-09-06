using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Tasks;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Tasks.V1.Commands;

public class UpdateTaskCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateTaskCommand, Ulid>
{
    public async Task<Ulid> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<TaskEntity, Ulid>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Task with Id '{command.Id}' not found");

        entity.AssetId = command.AssetId;
        entity.Type = command.Type;
        entity.Value = command.Value;

        unitOfWork.Repository<TaskEntity, Ulid>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
