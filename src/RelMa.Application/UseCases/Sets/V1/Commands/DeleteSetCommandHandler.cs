using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Sets;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Sets.V1.Commands;

public class DeleteSetCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteSetCommand, bool>
{
    public async Task<bool> Handle(DeleteSetCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<SetEntity, Ulid>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Set with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<SetEntity, Ulid>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
