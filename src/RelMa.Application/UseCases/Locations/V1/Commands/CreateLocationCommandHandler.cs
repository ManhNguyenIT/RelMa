using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Locations;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Locations.V1.Commands;

public sealed class CreateLocationCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateLocationCommand, Ulid>
{
    public async Task<Ulid> Handle(CreateLocationCommand command, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Repository<LocationEntity, Ulid>()
            .Find(x => !x.IsDeleted && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Location với tên {command.Name}");

        var entity = new LocationEntity()
        {
            Id = Ulid.NewUlid(),
            Name = command.Name,
        };
        unitOfWork.Repository<LocationEntity, Ulid>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
