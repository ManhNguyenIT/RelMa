using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Checklists;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Checklists.V1.Commands;

public sealed class DeleteChecklistCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteChecklistCommand, bool>
{
    public async Task<bool> Handle(DeleteChecklistCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<ChecklistEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Checklist with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<ChecklistEntity, DefaultIdType>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
