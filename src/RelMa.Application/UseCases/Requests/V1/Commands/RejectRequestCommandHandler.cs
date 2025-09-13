using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Requests;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public class RejectRequestCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<RejectRequestCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(RejectRequestCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<RequestEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Request with Id '{command.Id}' not found");

        entity.Status = Status.Accept;

        unitOfWork.Repository<RequestEntity, DefaultIdType>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
