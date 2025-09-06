using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Storages;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Storages.V1.Commands;

public class DeleteStorageCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteStorageCommand, bool>
{
    public async Task<bool> Handle(DeleteStorageCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<StorageEntity, Ulid>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Storage with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<StorageEntity, Ulid>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
