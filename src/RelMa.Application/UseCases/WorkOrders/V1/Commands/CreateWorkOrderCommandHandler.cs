using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Files;
using RelMa.Domain.WorkOrders;

namespace RelMa.Application.UseCases.WorkOrders.V1.Commands;

public sealed class CreateWorkOrderCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateWorkOrderCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreateWorkOrderCommand command, CancellationToken cancellationToken)
    {
        var entity = new WorkOrderEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            AssigneeId = command.AssigneeId,
            Category = command.Category,
            Description = command.Description,
            Estimate = command.Estimate,
            Title = command.Title,
            Files = await unitOfWork.Repository<FileEntity, DefaultIdType>()
                .Find(x => command.Files.Contains(x.Id))
                .ToListAsync(cancellationToken),
        };

        unitOfWork.Repository<WorkOrderEntity, DefaultIdType>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
