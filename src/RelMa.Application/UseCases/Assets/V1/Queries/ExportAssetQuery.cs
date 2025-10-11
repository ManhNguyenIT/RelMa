using Cortex.Mediator.Queries;
using RelMa.Shared.Abstractions.Query;

namespace RelMa.Application.UseCases.Assets.V1.Queries;

public sealed class ExportAssetQuery() : BaseQuery, IQuery<MemoryStream>;