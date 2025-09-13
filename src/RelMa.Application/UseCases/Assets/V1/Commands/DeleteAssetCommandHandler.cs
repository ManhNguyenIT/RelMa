using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Assets;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public class DeleteAssetCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteAssetCommand, bool>
{
    public async Task<bool> Handle(DeleteAssetCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<AssetEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Asset with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<AssetEntity, DefaultIdType>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
