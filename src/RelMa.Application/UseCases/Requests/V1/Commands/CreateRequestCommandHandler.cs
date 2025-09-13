using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Requests;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public sealed class CreateRequestCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateRequestCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreateRequestCommand command, CancellationToken cancellationToken)
    {
        var entity = new RequestEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            AssetId = command.AssetId,
            Description = command.Description,
            Priority = command.Priority,
            Status = command.Status,
            Title = command.Title,
            Image = command.Image,
        };

        unitOfWork.Repository<RequestEntity, DefaultIdType>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
