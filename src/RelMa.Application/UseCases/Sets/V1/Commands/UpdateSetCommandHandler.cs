using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Parts;
using RelMa.Domain.Sets;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Sets.V1.Commands;

public class UpdateSetCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateSetCommand, Ulid>
{
    public async Task<Ulid> Handle(UpdateSetCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<SetEntity, Ulid>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Set with Id '{command.Id}' not found");

        if (await unitOfWork.Repository<SetEntity, Ulid>()
            .Find(x => !x.IsDeleted && x.Id != command.Id && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Set với tên {command.Name}");

        entity.Name = command.Name;

        entity.AddParts(
            await unitOfWork.Repository<PartEntity, Ulid>()
                .Find(x => !x.IsDeleted && command.Parts.Contains(x.Id)).ToListAsync(cancellationToken));

        unitOfWork.Repository<SetEntity, Ulid>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
