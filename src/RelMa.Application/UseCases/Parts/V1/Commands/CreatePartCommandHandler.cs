using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Parts;

namespace RelMa.Application.UseCases.Parts.V1.Commands;

public sealed class CreatePartCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreatePartCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreatePartCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<PartEntity, DefaultIdType>()
            .FindSingleAsync(
                predicate: x => !x.IsDeleted && x.MaterialId == command.MaterialId && x.LocationId == command.LocationId && x.StorageId == command.StorageId,
                cancellationToken: cancellationToken)
            ?? new PartEntity()
            {
                Id = DefaultIdType.CreateVersion7(),
                MaterialId = command.MaterialId,
                LocationId = command.LocationId,
                StorageId = command.StorageId,
            };

        entity.Category = command.Category ?? entity.Category;
        entity.Description = command.Description ?? entity.Description;
        entity.Cost += command.Cost ?? 0;
        entity.Inventory += command.Inventory ?? 0;
        entity.Quantity += command.Quantity ?? 0;
        entity.Minimum = command.Minimum ?? entity.Minimum;
        entity.Status = command.Status ?? entity.Status;

        unitOfWork.Repository<PartEntity, DefaultIdType>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
