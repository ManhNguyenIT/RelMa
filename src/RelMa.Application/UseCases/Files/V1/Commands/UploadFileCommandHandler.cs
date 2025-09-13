using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Abstractions.Services;
using RelMa.Domain.Files;

namespace RelMa.Application.UseCases.Files.V1.Commands;

public class UploadFileCommandHandler(
    IUnitOfWork unitOfWork,
    IFileService fileService) : ICommandHandler<UploadFileCommand, IEnumerable<DefaultIdType>>
{
    public async Task<IEnumerable<DefaultIdType>> Handle(UploadFileCommand command, CancellationToken cancellationToken)
    {
        var result = new List<DefaultIdType>();

        var objectIds = await fileService.UploadFilesAsync(command.Files, cancellationToken);
        if (objectIds is null)
            return result;

        objectIds = await fileService.CopyFilesAsync(string.Empty, [.. objectIds], cancellationToken);

        var entities = objectIds.Select((objectId, index) =>
        {
            var file = command.Files[index];

            var entity = new FileEntity
            {
                Id = DefaultIdType.CreateVersion7(),
                Name = file.FileName,
                Ext = Path.GetExtension(file.FileName),
                Size = file.Length,
                Source = objectId,
            };
            return entity;
        }).ToList();

        foreach (var entity in entities)
        {
            unitOfWork.Repository<FileEntity, DefaultIdType>().Add(entity);
            result.Add(entity.Id);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await fileService.DeleteTempFilesAsync([.. objectIds], cancellationToken);

        return result;
    }
}
