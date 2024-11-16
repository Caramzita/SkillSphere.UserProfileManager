using FluentValidation;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.AddHistory;

public class AddHistoryCommandValidator : AbstractValidator<AddHistoryCommand>
{
    public AddHistoryCommandValidator()
    {
        RuleFor(command => command.CourseTitle)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Course title is required.")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
    }
}
