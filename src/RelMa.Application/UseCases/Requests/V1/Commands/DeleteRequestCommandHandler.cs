using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Requests;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public class DeleteRequestCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteRequestCommand, bool>
{
    public async Task<bool> Handle(DeleteRequestCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<RequestEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Request with Id '{command.Id}' not found");

        entity.Delete();
        unitOfWork.Repository<RequestEntity, DefaultIdType>().Update(entity);
        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
