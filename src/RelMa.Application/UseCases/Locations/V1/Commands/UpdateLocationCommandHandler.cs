using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Locations;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Locations.V1.Commands;

public class UpdateLocationCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateLocationCommand, Ulid>
{
    public async Task<Ulid> Handle(UpdateLocationCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<LocationEntity, Ulid>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Location with Id '{command.Id}' not found");

        if (await unitOfWork.Repository<LocationEntity, Ulid>()
            .Find(x => !x.IsDeleted && x.Id != command.Id && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Location với tên {command.Name}");

        entity.Name = command.Name;
        unitOfWork.Repository<LocationEntity, Ulid>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
