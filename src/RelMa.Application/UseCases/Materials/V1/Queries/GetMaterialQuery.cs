using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Materials.V1.Responses;
using RelMa.Shared;
using RelMa.Shared.Abstractions.Query;

namespace RelMa.Application.UseCases.Materials.V1.Queries;

public sealed class GetMaterialQuery : PaginationQuery, IQuery<PagedResult<MaterialResponse>>;
