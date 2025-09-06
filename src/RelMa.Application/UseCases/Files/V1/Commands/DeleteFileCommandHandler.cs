using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Files;

namespace RelMa.Application.UseCases.Files.V1.Commands;

public sealed class DeleteFileCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteFileCommand, bool>
{
    public async Task<bool> Handle(DeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<FileEntity, Ulid>()
            .Find(x => !x.IsDeleted && command.Ids.Contains(x.Id))
            .ToListAsync(cancellationToken);

        foreach (var item in entities)
        {
            item.Delete();
        }

        return await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
