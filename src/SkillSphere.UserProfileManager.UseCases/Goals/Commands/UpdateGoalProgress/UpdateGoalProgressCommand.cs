using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Enums;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.UpdateGoalProgress;

public class UpdateGoalProgressCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; }

    public Guid UserId {  get; }

    public GoalProgress Progress { get; }

    public UpdateGoalProgressCommand(Guid id, Guid userId, GoalProgress progress)
    {
        Id = id;
        UserId = userId;
        Progress = progress;
    }
}
