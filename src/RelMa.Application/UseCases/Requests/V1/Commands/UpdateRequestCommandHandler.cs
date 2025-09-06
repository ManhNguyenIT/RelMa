using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Requests;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public class UpdateRequestCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateRequestCommand, Ulid>
{
    public async Task<Ulid> Handle(UpdateRequestCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<RequestEntity, Ulid>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Request with Id '{command.Id}' not found");

        entity.AssetId = command.AssetId;
        entity.Description = command.Description;
        entity.Image = command.Image;
        entity.Priority = command.Priority;
        entity.Status = command.Status;
        entity.Title = command.Title;

        unitOfWork.Repository<RequestEntity, Ulid>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
