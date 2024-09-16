using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkill;

public class GetSkillQuery : IRequest<Result<Skill>>
{
    public Guid Id { get; }

    public Guid UserId { get; }

    public GetSkillQuery(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}
