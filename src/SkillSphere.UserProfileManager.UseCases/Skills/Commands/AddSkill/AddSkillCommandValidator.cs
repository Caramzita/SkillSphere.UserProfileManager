using FluentValidation;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;
public class AddSkillCommandValidator : AbstractValidator<AddSkillCommand>
{
    public AddSkillCommandValidator()
    {
        RuleFor(command => command.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("Name must not exceed 100 characters.");
    }
}
