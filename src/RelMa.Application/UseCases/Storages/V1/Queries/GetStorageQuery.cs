using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Storages.V1.Responses;
using RelMa.Shared.Abstractions.Query;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Storages.V1.Queries;

public sealed class GetStorageQuery : PaginationQuery, IQuery<PagedResult<StorageResponse>>;
