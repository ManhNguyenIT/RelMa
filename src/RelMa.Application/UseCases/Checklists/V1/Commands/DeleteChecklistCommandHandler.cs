using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Checklists;

namespace RelMa.Application.UseCases.Checklists.V1.Commands;

public sealed class DeleteChecklistCommandHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : ICommandHandler<DeleteChecklistCommand, bool>
{
    public async Task<bool> Handle(DeleteChecklistCommand command, CancellationToken cancellationToken)
        => await unitOfWork.Repository<ChecklistEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && command.Ids.Contains(x.Id))
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.IsDeleted, true)
                .SetProperty(p => p.DeletedAt, DateTimeOffset.UtcNow)
                .SetProperty(p => p.DeletedBy, userContext.UserId),
                cancellationToken: cancellationToken) > 0;
}
