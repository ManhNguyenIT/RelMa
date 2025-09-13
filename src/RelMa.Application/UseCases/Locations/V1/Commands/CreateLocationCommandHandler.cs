using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Locations;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Locations.V1.Commands;

public sealed class CreateLocationCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateLocationCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreateLocationCommand command, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Repository<LocationEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Location với tên {command.Name}");

        var entity = new LocationEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            Name = command.Name,
        };
        unitOfWork.Repository<LocationEntity, DefaultIdType>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
