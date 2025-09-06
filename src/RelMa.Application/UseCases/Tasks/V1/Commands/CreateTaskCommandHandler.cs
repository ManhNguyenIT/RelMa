using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Tasks;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Tasks.V1.Commands;

public sealed class CreateTaskCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateTaskCommand, Ulid>
{
    public async Task<Ulid> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var entity = new TaskEntity()
        {
            Id = Ulid.NewUlid(),
            AssetId = command.AssetId,
            Type = command.Type,
            Value = command.Value,
        };
        unitOfWork.Repository<TaskEntity, Ulid>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
