using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Materials;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Materials.V1.Commands;

public sealed class DeleteMaterialCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteMaterialCommand, bool>
{
    public async Task<bool> Handle(DeleteMaterialCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<MaterialEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Material with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<MaterialEntity, DefaultIdType>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
