using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Requests;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public class DeleteRequestCommandHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : ICommandHandler<DeleteRequestCommand, bool>
{
    public async Task<bool> Handle(DeleteRequestCommand command, CancellationToken cancellationToken)
        => await unitOfWork.Repository<RequestEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && command.Ids.Contains(x.Id))
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.IsDeleted, true)
                .SetProperty(p => p.DeletedAt, DateTimeOffset.UtcNow)
                .SetProperty(p => p.DeletedBy, userContext.UserId),
                cancellationToken: cancellationToken) > 0;
}
