using FluentValidation;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.AddGoal;

public class AddGoalCommandValidator : AbstractValidator<AddGoalCommand>
{
    public AddGoalCommandValidator()
    {
        RuleFor(command => command.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
    }
}
