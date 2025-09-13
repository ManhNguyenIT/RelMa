using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Tasks;

namespace RelMa.Application.UseCases.Tasks.V1.Commands;

public sealed class CreateTaskCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateTaskCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var entity = new TaskEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            AssetId = command.AssetId,
            Type = command.Type,
            Value = command.Value,
        };
        unitOfWork.Repository<TaskEntity, DefaultIdType>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
