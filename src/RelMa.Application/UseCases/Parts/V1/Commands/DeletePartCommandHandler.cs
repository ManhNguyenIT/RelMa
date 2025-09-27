using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Parts;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Parts.V1.Commands;

public sealed class DeletePartCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeletePartCommand, bool>
{
    public async Task<bool> Handle(DeletePartCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<PartEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Part with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<PartEntity, DefaultIdType>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
