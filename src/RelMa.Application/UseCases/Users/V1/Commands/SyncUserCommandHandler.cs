using Cortex.Mediator.Commands;
using RelMa.Application.Abstractions.Authentication;
using RelMa.Application.Abstractions.Database;
using RelMa.Application.UseCases.Users.V1.Responses;
using RelMa.Domain.Users;

namespace RelMa.Application.UseCases.Users.V1.Commands;

public class SyncUserCommandHandler(
    IUnitOfWork unitOfWork,
    IUserContext userContext) : ICommandHandler<SyncUserCommand, UserResponse>
{
    public async Task<UserResponse> Handle(SyncUserCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<UserEntity, DefaultIdType>()
            .FindByIdAsync(userContext.UserId, cancellationToken: cancellationToken);

        if (entity is null)
        {
            entity = new UserEntity
            {
                Id = userContext.UserId,
                Name = userContext.Name,
                Username = userContext.Username
            };
            unitOfWork.Repository<UserEntity, DefaultIdType>().Add(entity);
        }
        else
        {
            entity.Name = userContext.Name;
            entity.Username = userContext.Username;
            unitOfWork.Repository<UserEntity, DefaultIdType>().Update(entity);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UserResponse() { Id = entity.Id, Name = entity.Name, Username = entity.Username };
    }
}
