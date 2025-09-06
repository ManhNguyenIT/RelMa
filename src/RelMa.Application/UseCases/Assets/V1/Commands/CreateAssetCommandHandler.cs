using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Assets;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public sealed class CreateAssetCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateAssetCommand, Ulid>
{
    public async Task<Ulid> Handle(CreateAssetCommand command, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Repository<AssetEntity, Ulid>()
            .Find(x => !x.IsDeleted && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Asset với tên {command.Name}");

        var entity = new AssetEntity()
        {
            Id = Ulid.NewUlid(),
            Name = command.Name,
            LocationId = command.LocationId,
            Area = command.Area,
            Barcode = command.Barcode,
            Category = command.Category,
            Description = command.Description,
            ManufacturerId = command.ManufacturerId,
            Model = command.Model,
            SerialNumber = command.SerialNumber,
        };
        unitOfWork.Repository<AssetEntity, Ulid>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
