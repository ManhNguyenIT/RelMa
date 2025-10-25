using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Assets;

namespace RelMa.Application.UseCases.Assets.V1.Commands;

public class DeleteAssetCommandHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : ICommandHandler<DeleteAssetCommand, bool>
{
    public async Task<bool> Handle(DeleteAssetCommand command, CancellationToken cancellationToken)
        => await unitOfWork.Repository<AssetEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && command.Ids.Contains(x.Id))
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.IsDeleted, true)
                .SetProperty(p => p.DeletedAt, DateTimeOffset.UtcNow)
                .SetProperty(p => p.DeletedBy, userContext.UserId),
                cancellationToken: cancellationToken) > 0;
}
