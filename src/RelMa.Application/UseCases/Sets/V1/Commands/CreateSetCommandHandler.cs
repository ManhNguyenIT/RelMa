using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Parts;
using RelMa.Domain.Sets;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Sets.V1.Commands;

public sealed class CreateSetCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateSetCommand, Ulid>
{
    public async Task<Ulid> Handle(CreateSetCommand command, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Repository<SetEntity, Ulid>()
            .Find(x => !x.IsDeleted && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Set với tên {command.Name}");

        var entity = new SetEntity()
        {
            Id = Ulid.NewUlid(),
            Name = command.Name,
        };

        entity.AddParts(
            await unitOfWork.Repository<PartEntity, Ulid>()
                .Find(x => !x.IsDeleted && command.Parts.Contains(x.Id)).ToListAsync(cancellationToken));

        unitOfWork.Repository<SetEntity, Ulid>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
