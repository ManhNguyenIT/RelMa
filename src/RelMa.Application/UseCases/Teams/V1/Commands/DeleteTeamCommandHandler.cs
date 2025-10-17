using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Teams;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Teams.V1.Commands;

public sealed class DeleteTeamCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteTeamCommand, bool>
{
    public async Task<bool> Handle(DeleteTeamCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<TeamEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Team with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<TeamEntity, DefaultIdType>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
