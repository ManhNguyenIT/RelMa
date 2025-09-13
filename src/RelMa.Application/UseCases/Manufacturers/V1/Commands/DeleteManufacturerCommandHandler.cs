using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Manufacturers;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Manufacturers.V1.Commands;

public class DeleteManufacturerCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteManufacturerCommand, bool>
{
    public async Task<bool> Handle(DeleteManufacturerCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<ManufacturerEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Manufacturer with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<ManufacturerEntity, DefaultIdType>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
