using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Storages;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Storages.V1.Commands;

public sealed class CreateStorageCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateStorageCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreateStorageCommand command, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Repository<StorageEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Storage với tên {command.Name}");

        var entity = new StorageEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            Name = command.Name,
        };

        unitOfWork.Repository<StorageEntity, DefaultIdType>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
