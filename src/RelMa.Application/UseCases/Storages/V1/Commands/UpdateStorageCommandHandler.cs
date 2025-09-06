using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Storages;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Storages.V1.Commands;

public class UpdateStorageCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateStorageCommand, Ulid>
{
    public async Task<Ulid> Handle(UpdateStorageCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<StorageEntity, Ulid>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Storage with Id '{command.Id}' not found");

        if (await unitOfWork.Repository<StorageEntity, Ulid>()
            .Find(x => !x.IsDeleted && x.Id != command.Id && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Storage với tên {command.Name}");

        entity.Name = command.Name;

        unitOfWork.Repository<StorageEntity, Ulid>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
