using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Locations;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Locations.V1.Commands;

public class UpdateLocationCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateLocationCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(UpdateLocationCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<LocationEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Location with Id '{command.Id}' not found");

        if (await unitOfWork.Repository<LocationEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && x.Id != command.Id && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Location với tên {command.Name}");

        entity.Name = command.Name;
        entity.Description = command.Description;
        unitOfWork.Repository<LocationEntity, DefaultIdType>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
