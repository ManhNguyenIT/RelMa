using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Assets;
using RelMa.Domain.Requests;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public sealed class CreateRequestCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateRequestCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreateRequestCommand command, CancellationToken cancellationToken)
    {
        var asset = await unitOfWork.Repository<AssetEntity, DefaultIdType>()
            .FindByIdAsync(command.AssetId, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Asset with Id '{command.AssetId}' not found.");

        var entity = new RequestEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            AssetId = asset.Id,
            Title = command.Title,
            Description = command.Description,
            Status = command.Status,
            Category = command.Category,
            Priority = command.Priority,
            Images = command.Images,
            TenantId = asset.TenantId,
        };

        unitOfWork.Repository<RequestEntity, DefaultIdType>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
