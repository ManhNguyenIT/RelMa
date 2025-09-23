using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Parts;
using RelMa.Domain.Sets;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Sets.V1.Commands;

public class UpdateSetCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateSetCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(UpdateSetCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<SetEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Set with Id '{command.Id}' not found");

        if (await unitOfWork.Repository<SetEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && x.Id != command.Id && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Set với tên {command.Name}");

        entity.Name = command.Name;
        entity.SetParts(await unitOfWork.Repository<PartEntity, DefaultIdType>()
                .Find(x => !x.IsDeleted && command.Parts.Contains(x.Id)).ToListAsync(cancellationToken));

        unitOfWork.Repository<SetEntity, DefaultIdType>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
