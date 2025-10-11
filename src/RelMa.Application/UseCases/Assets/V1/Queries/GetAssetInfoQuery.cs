using Cortex.Mediator.Queries;
using RelMa.Application.UseCases.Assets.V1.Responses;

namespace RelMa.Application.UseCases.Assets.V1.Queries;

public sealed record GetAssetInfoQuery(DefaultIdType Id) : IQuery<AssetResponse>;
