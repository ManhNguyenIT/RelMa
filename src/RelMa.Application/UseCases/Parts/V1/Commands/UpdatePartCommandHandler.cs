using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Parts;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Parts.V1.Commands;

public sealed class UpdatePartCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdatePartCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(UpdatePartCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<PartEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Part with Id '{command.Id}' not found");

        if (entity.IsDeleted)
            return entity.Id;

        var target = await unitOfWork.Repository<PartEntity, DefaultIdType>()
            .FindSingleAsync(
                predicate: x => !x.IsDeleted && x.Id != entity.Id && x.MaterialId == command.MaterialId && x.LocationId == command.LocationId && x.StorageId == command.StorageId,
                cancellationToken: cancellationToken);

        if (target is not null)
        {
            target.Quantity += entity.Quantity;
            target.Inventory += entity.Inventory;
            target.Cost += entity.Cost;
            entity.Category = command.Category ?? entity.Category;
            entity.Description = command.Description ?? entity.Description;
            entity.Minimum = command.Minimum ?? entity.Minimum;
            entity.Status = command.Status ?? entity.Status;

            unitOfWork.Repository<PartEntity, DefaultIdType>().Update(target);

            entity.Delete();
            unitOfWork.Repository<PartEntity, DefaultIdType>().Update(entity);

            return target.Id;
        }


        entity.MaterialId = command.MaterialId;
        entity.LocationId = command.LocationId;
        entity.StorageId = command.StorageId;
        entity.Category = command.Category ?? entity.Category;
        entity.Description = command.Description ?? entity.Description;
        entity.Cost += command.Cost ?? 0;
        entity.Inventory += command.Inventory ?? 0;
        entity.Quantity += command.Quantity ?? 0;
        entity.Minimum = command.Minimum ?? entity.Minimum;
        entity.Status = command.Status ?? entity.Status;

        unitOfWork.Repository<PartEntity, DefaultIdType>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
