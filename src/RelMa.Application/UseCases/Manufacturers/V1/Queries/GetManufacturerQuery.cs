using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Manufacturers.V1.Responses;
using RelMa.Shared.Abstractions.Query;
using System.Linq.Dynamic.Core;

namespace RelMa.Application.UseCases.Manufacturers.V1.Queries;

public sealed class GetManufacturerQuery : PaginationQuery, IQuery<PagedResult<ManufacturerResponse>>;
