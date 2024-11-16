using FluentValidation;

namespace SkillSphere.UserProfileManager.UseCases.UserSkills.Commands.AddUserSkills;

public class AddUserSkillsCommandValidator : AbstractValidator<AddUserSkillsCommand>
{
    public AddUserSkillsCommandValidator()
    {
        RuleFor(command => command.SkillIds)
            .Must(skillIds => skillIds == null || skillIds.All(id => id != Guid.Empty))
            .WithMessage("Skill IDs must not contain empty GUIDs.");
    }
}
