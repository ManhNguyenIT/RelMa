using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Items;
using RelMa.Domain.Parts;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Parts.V1.Commands;

public class UpdatePartCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdatePartCommand, Ulid>
{
    public async Task<Ulid> Handle(UpdatePartCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<PartEntity, Ulid>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Part with Id '{command.Id}' not found");

        if (await unitOfWork.Repository<PartEntity, Ulid>()
            .Find(x => !x.IsDeleted && x.Id != command.Id && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Part với tên {command.Name}");

        entity.Name = command.Name;
        entity.Category = command.Category;
        entity.Cost = command.Cost;
        entity.Description = command.Description;
        entity.Image = command.Image;
        entity.PartNumber = command.PartNumber;
        entity.Quantity = command.Quantity;

        entity.AddItems(
            await unitOfWork.Repository<ItemEntity, Ulid>()
                .Find(x => !x.IsDeleted && command.Items.Contains(x.Id)).ToListAsync(cancellationToken));

        unitOfWork.Repository<PartEntity, Ulid>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
