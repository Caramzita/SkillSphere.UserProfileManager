using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetAllSkills;

public class GetAllSkillsQuery : IRequest<Result<IEnumerable<Skill>>> 
{ 
    public Guid UserId { get; }

    public GetAllSkillsQuery(Guid userId)
    {
        UserId = userId;
    }
}
