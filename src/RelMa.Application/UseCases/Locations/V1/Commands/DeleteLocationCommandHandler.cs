using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Locations;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Locations.V1.Commands;

public class DeleteLocationCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteLocationCommand, bool>
{
    public async Task<bool> Handle(DeleteLocationCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<LocationEntity, Ulid>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Location with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<LocationEntity, Ulid>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
