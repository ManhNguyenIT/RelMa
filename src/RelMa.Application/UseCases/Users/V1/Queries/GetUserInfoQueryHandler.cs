using Cortex.Mediator.Queries;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.UseCases.Users.V1.Responses;
using RelMa.Domain.Users;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Users.V1.Queries;

public sealed class GetUserInfoQueryHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : IQueryHandler<GetUserInfoQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<UserEntity, DefaultIdType>()
            .FindByIdAsync(userContext.UserId, cancellationToken: cancellationToken)
            ?? throw new NotFoundException("User not found");

        return new UserResponse()
        {
            Id = entity.Id,
            Company = entity.Company,
            Name = entity.Name,
            PhoneNumber = entity.PhoneNumber,
            Username = entity.Username
        };
    }
}
