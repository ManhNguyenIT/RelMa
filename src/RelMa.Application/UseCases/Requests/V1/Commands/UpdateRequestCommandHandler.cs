using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Assets;
using RelMa.Domain.Requests;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public class UpdateRequestCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateRequestCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(UpdateRequestCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<RequestEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Request with Id '{command.Id}' not found");

        if (entity.AssetId != command.AssetId)
        {
            var asset = await unitOfWork.Repository<AssetEntity, DefaultIdType>()
                .FindByIdAsync(command.AssetId, cancellationToken: cancellationToken)
                ?? throw new NotFoundException($"Asset with Id '{command.AssetId}' not found.");

            entity.AssetId = asset.Id;
            entity.TenantId = asset.TenantId;
        }

        entity.Title = command.Title;
        entity.Description = command.Description;
        entity.Status = command.Status;
        entity.Category = command.Category;
        entity.Priority = command.Priority;
        entity.Images = command.Images;

        unitOfWork.Repository<RequestEntity, DefaultIdType>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
