using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Items;
using RelMa.Domain.Parts;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Parts.V1.Commands;

public sealed class CreatePartCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreatePartCommand, Ulid>
{
    public async Task<Ulid> Handle(CreatePartCommand command, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Repository<PartEntity, Ulid>()
            .Find(x => !x.IsDeleted && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Part với tên {command.Name}");

        var entity = new PartEntity()
        {
            Id = Ulid.NewUlid(),
            Name = command.Name,
            Category = command.Category,
            Cost = command.Cost,
            Description = command.Description,
            Image = command.Image,
            PartNumber = command.PartNumber,
            Quantity = command.Quantity,
        };

        entity.AddItems(
            await unitOfWork.Repository<ItemEntity, Ulid>()
                .Find(x => !x.IsDeleted && command.Items.Contains(x.Id)).ToListAsync(cancellationToken));

        unitOfWork.Repository<PartEntity, Ulid>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
