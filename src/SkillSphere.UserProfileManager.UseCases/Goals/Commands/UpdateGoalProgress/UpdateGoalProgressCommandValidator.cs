using FluentValidation;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.UpdateGoalProgress;

public class UpdateGoalProgressCommandValidator : AbstractValidator<UpdateGoalProgressCommand>
{
    public UpdateGoalProgressCommandValidator()
    {
        RuleFor(command => command.Progress)
            .IsInEnum().WithMessage("Progress must be a valid value.");
    }
}
