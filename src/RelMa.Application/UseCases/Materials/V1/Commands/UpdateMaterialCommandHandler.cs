using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Materials;
using RelMa.Domain.Parts;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Materials.V1.Commands;

public sealed class UpdateMaterialCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateMaterialCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(UpdateMaterialCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<MaterialEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Material with Id '{command.Id}' not found");

        if (await unitOfWork.Repository<MaterialEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && x.Id != command.Id && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Material với tên {command.Name}");

        entity.Name = command.Name;
        entity.Code = command.Code;
        entity.Description = command.Description;
        entity.Images = command.Images;
        entity.Status = command.Status;
        entity.SetParts(await unitOfWork.Repository<PartEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && command.Parts.Contains(x.Id)).ToListAsync(cancellationToken));

        unitOfWork.Repository<MaterialEntity, DefaultIdType>().Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
