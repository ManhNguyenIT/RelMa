using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Assets.V1.Responses;
using RelMa.Shared.Abstractions.Query;

namespace RelMa.Application.UseCases.Assets.V1.Queries;

public sealed class GetAssetQuery : PaginationQuery, IQuery<Shared.PagedResult<AssetResponse>>;
