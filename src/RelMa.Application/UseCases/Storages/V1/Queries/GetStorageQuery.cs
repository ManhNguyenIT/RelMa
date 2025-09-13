using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Storages.V1.Responses;
using RelMa.Shared.Abstractions.Query;

namespace RelMa.Application.UseCases.Storages.V1.Queries;

public sealed class GetStorageQuery : PaginationQuery, IQuery<Shared.PagedResult<StorageResponse>>;
