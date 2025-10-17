using Cortex.Mediator.Queries;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.Extentions;
using RelMa.Application.UseCases.Teams.V1.Responses;
using RelMa.Application.UseCases.Users.V1.Responses;
using RelMa.Domain.Teams;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Teams.V1.Queries;

public sealed class GetTeamQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetTeamQuery, Shared.PagedResult<TeamResponse>>
{
    public async Task<Shared.PagedResult<TeamResponse>> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Repository<TeamEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted)
            .Select(x => new TeamResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                LeaderId = x.LeaderId,
                Leader = x.Leader == null ? null : new UserResponse()
                {
                    Id = x.Leader.Id,
                    Name = x.Leader.Name,
                },
                Members = x.Members == null ? null : x.Members.Select(m => new UserResponse()
                {
                    Id = m.Id,
                    Name = m.Name,
                }).ToList()
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
