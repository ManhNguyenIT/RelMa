using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Manufacturers;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Manufacturers.V1.Commands;

public sealed class CreateManufacturerCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateManufacturerCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreateManufacturerCommand command, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Repository<ManufacturerEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Manufacturer với tên {command.Name}");

        var entity = new ManufacturerEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            Name = command.Name,
        };
        unitOfWork.Repository<ManufacturerEntity, DefaultIdType>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
