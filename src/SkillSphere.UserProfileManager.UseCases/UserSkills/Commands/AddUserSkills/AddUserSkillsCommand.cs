using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserSkill;

namespace SkillSphere.UserProfileManager.UseCases.UserSkills.Commands.AddUserSkills;

public class AddUserSkillsCommand : IRequest<Result<List<UserSkillResponseDto>>>
{
    public Guid UserId { get; }

    public List<Guid> SkillIds { get; }

    public AddUserSkillsCommand(Guid userId, List<Guid> skillIds)
    {
        UserId = userId;
        SkillIds = skillIds;
    }
}
