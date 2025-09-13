using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Assets;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public class UpdateAssetCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateAssetCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(UpdateAssetCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<AssetEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Asset with Id '{command.Id}' not found");

        if (await unitOfWork.Repository<AssetEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && x.Id != command.Id && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Asset với tên {command.Name}");

        entity.Name = command.Name;
        entity.Area = command.Area;
        entity.Barcode = command.Barcode;
        entity.Category = command.Category;
        entity.Description = command.Description;
        entity.LocationId = command.LocationId;
        entity.ManufacturerId = command.ManufacturerId;
        entity.Model = command.Model;
        entity.SerialNumber = command.SerialNumber;

        unitOfWork.Repository<AssetEntity, DefaultIdType>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
