using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.WorkOrders;

namespace RelMa.Application.UseCases.WorkOrders.V1.Commands;

public class DeleteWorkOrderCommandHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : ICommandHandler<DeleteWorkOrderCommand, bool>
{
    public async Task<bool> Handle(DeleteWorkOrderCommand command, CancellationToken cancellationToken)
        => await unitOfWork.Repository<WorkOrderEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && command.Ids.Contains(x.Id))
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.IsDeleted, true)
                .SetProperty(p => p.DeletedAt, DateTimeOffset.UtcNow)
                .SetProperty(p => p.DeletedBy, userContext.UserId),
                cancellationToken: cancellationToken) > 0;
}
