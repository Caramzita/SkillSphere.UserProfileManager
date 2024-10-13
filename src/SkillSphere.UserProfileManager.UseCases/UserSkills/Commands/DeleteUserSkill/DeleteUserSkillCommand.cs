using MediatR;
using SkillSphere.Infrastructure.UseCases;

namespace SkillSphere.UserProfileManager.UseCases.UserSkills.Commands.DeleteUserSkill;

public class DeleteUserSkillCommand : IRequest<Result<Unit>>
{
    public Guid UserId { get; }

    public Guid SkillId { get; }

    public DeleteUserSkillCommand(Guid userId, Guid skillId)
    {
        UserId = userId;
        SkillId = skillId;
    }
}
