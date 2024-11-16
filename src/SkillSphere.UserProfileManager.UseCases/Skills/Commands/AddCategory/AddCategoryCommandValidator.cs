using FluentValidation;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddCategory;
public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryCommandValidator()
    {
        RuleFor(command => command.CategoryName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Category name is required.")
            .MinimumLength(2).WithMessage("Category name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("Category name must not exceed 100 characters.");
    }
}
