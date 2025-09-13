using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Manufacturers;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Manufacturers.V1.Commands;

public class UpdateManufacturerCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateManufacturerCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(UpdateManufacturerCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<ManufacturerEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Manufacturer with Id '{command.Id}' not found");

        if (await unitOfWork.Repository<ManufacturerEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && x.Id != command.Id && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Manufacturer với tên {command.Name}");

        entity.Name = command.Name;
        unitOfWork.Repository<ManufacturerEntity, DefaultIdType>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
