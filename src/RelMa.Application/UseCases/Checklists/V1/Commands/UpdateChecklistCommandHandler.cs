using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Checklists;
using RelMa.Domain.Tasks;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Checklists.V1.Commands;

public sealed class UpdateChecklistCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateChecklistCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(UpdateChecklistCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<ChecklistEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Checklist with Id '{command.Id}' not found");


        entity.Name = command.Name;
        entity.Description = command.Description;
        entity.WorkOrderId = command.WorkOrderId;

        entity.SetTasks(await unitOfWork.Repository<TaskEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && command.Tasks.Contains(x.Id))
            .ToListAsync(cancellationToken: cancellationToken));

        unitOfWork.Repository<ChecklistEntity, DefaultIdType>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
