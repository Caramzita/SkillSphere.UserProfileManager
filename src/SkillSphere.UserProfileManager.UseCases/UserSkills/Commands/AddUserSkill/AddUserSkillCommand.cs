using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserSkill;

namespace SkillSphere.UserProfileManager.UseCases.UserSkills.Commands.AddUserSkill;

public class AddUserSkillCommand : IRequest<Result<UserSkillResponseDto>>
{
    public Guid UserId { get; }

    public Guid SkillId { get; }

    public AddUserSkillCommand(Guid userId, Guid skillId)
    {
        UserId = userId;
        SkillId = skillId;
    }
}
