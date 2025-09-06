using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Manufacturers;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Manufacturers.V1.Commands;

public sealed class CreateManufacturerCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateManufacturerCommand, Ulid>
{
    public async Task<Ulid> Handle(CreateManufacturerCommand command, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Repository<ManufacturerEntity, Ulid>()
            .Find(x => !x.IsDeleted && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Manufacturer với tên {command.Name}");

        var entity = new ManufacturerEntity()
        {
            Id = Ulid.NewUlid(),
            Name = command.Name,
        };
        unitOfWork.Repository<ManufacturerEntity, Ulid>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
