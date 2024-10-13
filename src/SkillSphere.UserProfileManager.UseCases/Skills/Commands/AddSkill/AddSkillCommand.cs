using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.Skill;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;

public class AddSkillCommand : IRequest<Result<SkillResponseDto>>
{
    public string Name { get; }

    public Guid CategoryId { get; }

    public AddSkillCommand(string name, Guid categoryId)
    {
        Name = name;
        CategoryId = categoryId;
    }
}
