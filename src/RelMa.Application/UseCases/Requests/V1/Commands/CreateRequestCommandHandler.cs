using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Requests;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public sealed class CreateRequestCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateRequestCommand, Ulid>
{
    public async Task<Ulid> Handle(CreateRequestCommand command, CancellationToken cancellationToken)
    {
        var entity = new RequestEntity()
        {
            Id = Ulid.NewUlid(),
            AssetId = command.AssetId,
            Description = command.Description,
            Priority = command.Priority,
            Status = command.Status,
            Title = command.Title,
            Image = command.Image,
        };

        unitOfWork.Repository<RequestEntity, Ulid>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
