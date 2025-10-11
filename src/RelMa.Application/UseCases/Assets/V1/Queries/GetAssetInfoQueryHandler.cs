using Cortex.Mediator.Queries;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.UseCases.Assets.V1.Responses;
using RelMa.Domain.Assets;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Assets.V1.Queries;

public sealed class GetAssetInfoQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetAssetInfoQuery, AssetResponse>
{
    public async Task<AssetResponse> Handle(GetAssetInfoQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<AssetEntity, DefaultIdType>()
            .FindByIdAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException("Asset not found");

        return new AssetResponse()
        {
            Id = entity.Id,
            LocationId = entity.LocationId,
            Name = entity.Name,
            Code = entity.Code,
            Description = entity.Description,
            Model = entity.Model,
            SerialNumber = entity.SerialNumber,
            Category = entity.Category,
            Area = entity.Area,
            Location = entity.Location is null ? null : new Locations.V1.Responses.LocationResponse()
            {
                Id = entity.Location.Id,
                Name = entity.Location.Name,
            }
        };
    }
}
