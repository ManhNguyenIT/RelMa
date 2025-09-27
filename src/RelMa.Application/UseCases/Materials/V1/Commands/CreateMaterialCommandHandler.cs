using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Materials;
using RelMa.Domain.Parts;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Materials.V1.Commands;

public sealed class CreateMaterialCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateMaterialCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreateMaterialCommand command, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Repository<MaterialEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Material với tên {command.Name}");

        var entity = new MaterialEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            Name = command.Name,
            Code = command.Code,
            Description = command.Description,
            Images = command.Images,
            Status = command.Status,
        };

        entity.SetParts(await unitOfWork.Repository<PartEntity, DefaultIdType>()
                .Find(x => !x.IsDeleted && command.Parts.Contains(x.Id)).ToListAsync(cancellationToken));

        unitOfWork.Repository<MaterialEntity, DefaultIdType>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
