using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.Goal;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.AddGoal;

public class AddGoalCommand : IRequest<Result<GoalResponseDto>>
{
    public Guid UserId { get; set; }

    public string Title { get; }

    public AddGoalCommand(string title)
    {
        Title = title;
    }
}
