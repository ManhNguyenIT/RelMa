using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Teams;
using RelMa.Domain.Users;
using RelMa.Shared.Exceptions;
using System.Linq;

namespace RelMa.Application.UseCases.Teams.V1.Commands;

public sealed class CreateTeamCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateTeamCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Repository<TeamEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Team với tên {command.Name}");

        var entity = new TeamEntity()
        {
            Id = DefaultIdType.CreateVersion7(),
            Name = command.Name,
            Description = command.Description,
            LeaderId = command.LeaderId
        };

        entity.SetMember(
            command.MemberIds.Length == 0
                ? []
                : await unitOfWork.Repository<UserEntity, DefaultIdType>()
                    .Find(x => !x.IsDeleted && command.MemberIds.Contains(x.Id))
                    .ToArrayAsync(cancellationToken)
        );

        unitOfWork.Repository<TeamEntity, DefaultIdType>().Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
