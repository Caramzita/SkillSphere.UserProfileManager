using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Enums;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.UpdateGoalProgress;

public class UpdateGoalProgressCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }

    public Guid UserId {  get; set; }

    public GoalProgress Progress { get; set; }

    public UpdateGoalProgressCommand(Guid id, Guid userId, GoalProgress progress)
    {
        Id = id;
        UserId = userId;
        Progress = progress;
    }
}
