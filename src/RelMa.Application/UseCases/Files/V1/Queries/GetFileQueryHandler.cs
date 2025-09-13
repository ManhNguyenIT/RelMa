using Cortex.Mediator.Queries;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Files.V1.Responses;
using RelMa.Domain.Files;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Files.V1.Queries;

public sealed class GetFileQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetFileQuery, Shared.PagedResult<FileResponse>>
{
    public async Task<Shared.PagedResult<FileResponse>> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<FileEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted)
            .Select(x => new FileResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Ext = x.Ext,
                Size = x.Size,
                Source = x.Source,
            });

        if (request.Includes?.Length > 0)
            query = query.Includes(request.Includes.Split(','));

        if (request.Filters?.Length > 0)
            query = query.Where(request.Filters);

        query = request.Orders?.Length > 0
            ? query.OrderBy(request.Orders)
            : query.OrderByDescending(o => o.Name);

        if (request.Columns?.Length > 0)
            query = query.Select(request.Columns.Split(','));

        return await query.ToPagedResultAsync(
            page: request.Page,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);
    }
}
