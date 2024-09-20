using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Queries.GetAllGoals;

public class GetAllGoalsQuery : IRequest<Result<IEnumerable<Goal>>>
{
    public Guid UserId { get; }

    public GetAllGoalsQuery(Guid userId)
    {
        UserId = userId;
    }
}
