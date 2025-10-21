using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Teams;

namespace RelMa.Application.UseCases.Teams.V1.Commands;

public sealed class DeleteTeamCommandHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : ICommandHandler<DeleteTeamCommand, bool>
{
    public async Task<bool> Handle(DeleteTeamCommand command, CancellationToken cancellationToken)
        => await unitOfWork.Repository<TeamEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && command.Ids.Contains(x.Id))
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.IsDeleted, true)
                .SetProperty(p => p.DeletedAt, DateTimeOffset.UtcNow)
                .SetProperty(p => p.DeletedBy, userContext.UserId),
                cancellationToken: cancellationToken) > 0;
}
