using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Checklists;
using RelMa.Domain.Tasks;

namespace RelMa.Application.UseCases.Checklists.V1.Commands;

public sealed class CreateChecklistCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<CreateChecklistCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreateChecklistCommand command, CancellationToken cancellationToken)
    {
        var entity = new ChecklistEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            Name = command.Name,
            WorkOrderId = command.WorkOrderId,
            Description = command.Description,
        };

        entity.SetTasks(await unitOfWork.Repository<TaskEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && command.Tasks.Contains(x.Id))
            .ToListAsync(cancellationToken: cancellationToken));

        unitOfWork.Repository<ChecklistEntity, Guid>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);

        return entity.Id;
    }
}
