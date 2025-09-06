using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Manufacturers;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Manufacturers.V1.Commands;

public class UpdateManufacturerCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateManufacturerCommand, Ulid>
{
    public async Task<Ulid> Handle(UpdateManufacturerCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<ManufacturerEntity, Ulid>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Manufacturer with Id '{command.Id}' not found");

        if (await unitOfWork.Repository<ManufacturerEntity, Ulid>()
            .Find(x => !x.IsDeleted && x.Id != command.Id && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Manufacturer với tên {command.Name}");

        entity.Name = command.Name;
        unitOfWork.Repository<ManufacturerEntity, Ulid>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
