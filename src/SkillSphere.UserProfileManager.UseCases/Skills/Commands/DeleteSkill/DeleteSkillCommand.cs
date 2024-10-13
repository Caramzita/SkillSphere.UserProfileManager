using MediatR;
using SkillSphere.Infrastructure.UseCases;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteSkill;

public class DeleteSkillCommand : IRequest<Result<Unit>>
{
    public Guid CategoryId { get; }

    public Guid SkillId { get; }

    public DeleteSkillCommand(Guid categoryId, Guid skillId)
    {
        CategoryId = categoryId;
        SkillId = skillId;
    }
}
