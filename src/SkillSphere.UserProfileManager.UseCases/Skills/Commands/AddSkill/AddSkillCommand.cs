using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Enums;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;

public class AddSkillCommand : IRequest<Result<Skill>>
{
    public Guid UserId { get; set; }

    public string Name { get; }

    public SkillLevel Level { get; }

    public AddSkillCommand(string name, SkillLevel level)
    {
        Name = name;
        Level = level;
    }
}
