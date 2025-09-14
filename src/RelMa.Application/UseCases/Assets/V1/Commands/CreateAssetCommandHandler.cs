using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Assets;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public sealed class CreateAssetCommandHandler(
    IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateAssetCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreateAssetCommand command, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Repository<AssetEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Asset với tên {command.Name}");

        var entity = new AssetEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            Name = command.Name,
            LocationId = command.LocationId,
            Area = command.Area,
            Barcode = command.Barcode,
            Category = command.Category,
            Description = command.Description,
            ManufacturerId = command.ManufacturerId,
            Model = command.Model,
            SerialNumber = command.SerialNumber,
            Images = command.Images
        };

        unitOfWork.Repository<AssetEntity, DefaultIdType>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
