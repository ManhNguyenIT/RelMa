using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;
using RelMa.Application.Abstractions.Database;
using RelMa.Domain.Teams;
using RelMa.Domain.Users;
using RelMa.Shared.Exceptions;

namespace RelMa.Application.UseCases.Teams.V1.Commands;

public sealed class UpdateTeamCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateTeamCommand, DefaultIdType>
{
    public async Task<DefaultIdType> Handle(UpdateTeamCommand command, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<TeamEntity, DefaultIdType>()
            .FindByIdAsync(command.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"Team with Id '{command.Id}' not found");

        if (await unitOfWork.Repository<TeamEntity, DefaultIdType>()
            .Find(x => !x.IsDeleted && x.Id != entity.Id && x.Name == command.Name).AnyAsync(cancellationToken))
            throw new ConflictException($"Đã tồn tại Team với tên {command.Name}");

        entity.Name = command.Name;
        entity.Description = command.Description;
        entity.LeaderId = command.LeaderId;

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
