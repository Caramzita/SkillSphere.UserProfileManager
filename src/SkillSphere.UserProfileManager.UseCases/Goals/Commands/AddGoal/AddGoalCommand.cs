using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.AddGoal;

public class AddGoalCommand : IRequest<Result<Goal>>
{
    public Guid UserId { get; }

    public string Title { get; }

    public AddGoalCommand(Guid userId, string title)
    {
        UserId = userId;
        Title = title;
    }
}
