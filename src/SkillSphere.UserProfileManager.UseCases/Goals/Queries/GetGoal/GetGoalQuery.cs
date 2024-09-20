using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Queries.GetGoal;

public class GetGoalQuery : IRequest<Result<Goal>>
{
    public Guid Id { get; }

    public Guid UserId { get; }

    public GetGoalQuery(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}
