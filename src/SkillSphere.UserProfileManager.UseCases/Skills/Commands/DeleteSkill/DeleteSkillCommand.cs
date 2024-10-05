using MediatR;
using SkillSphere.Infrastructure.UseCases;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteSkill;

public class DeleteSkillCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; }

    public DeleteSkillCommand(Guid id)
    {
        Id = id;
    }
}
