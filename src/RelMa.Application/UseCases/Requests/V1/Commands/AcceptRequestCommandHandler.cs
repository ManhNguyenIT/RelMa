using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Requests;
using RelMa.Domain.WorkOrders;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Requests.V1.Commands;

public class AcceptRequestCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<AcceptRequestCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(AcceptRequestCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<RequestEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Request with Id '{command.Id}' not found");

        entity.Status = Domain.Requests.Status.Accept;

        unitOfWork.Repository<RequestEntity, DefaultIdType>().Update(entity);

        unitOfWork.Repository<WorkOrderEntity, DefaultIdType>().Add(new WorkOrderEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            RequestId = entity.Id,
            Status = Domain.WorkOrders.Status.Open,
        });

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
