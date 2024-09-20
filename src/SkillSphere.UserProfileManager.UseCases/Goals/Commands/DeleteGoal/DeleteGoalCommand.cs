using MediatR;
using SkillSphere.Infrastructure.UseCases;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.DeleteGoal;

public class DeleteGoalCommand : IRequest<Result<Unit>>
{
    public Guid UserId { get; }

    public Guid Id { get; }

    public DeleteGoalCommand(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}
