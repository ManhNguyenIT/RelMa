using Cortex.Mediator.Queries;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Abstractions.Services;
using RelMa.Domain.Files;
using RelMa.Shared.Exceptions;
using System.IO.Pipelines;

namespace RelMa.Application.UseCases.Files.V1.Queries;

public sealed class DownloadFileQueryHandler(
    IUnitOfWork unitOfWork,
    IFileService fileService) : IQueryHandler<DownloadFileQuery, PipeReader>
{
    public async Task<PipeReader> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.Repository<FileEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && request.Ids.Contains(x.Id))
            .Select(x => x.Source)
            .ToArrayAsync(cancellationToken);

        if (entities.Length == 0)
            throw new BadRequestException("Files not found.");

        return await fileService.DownloadFilesAsync(entities, cancellationToken);
    }
}
