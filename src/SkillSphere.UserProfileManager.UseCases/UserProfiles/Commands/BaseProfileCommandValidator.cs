using FluentValidation;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands;

public class BaseProfileCommandValidator<T> : AbstractValidator<T>
    where T : IProfileCommand
{
    protected BaseProfileCommandValidator()
    {
        RuleFor(command => command.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

        RuleFor(command => command.Bio)
            .MaximumLength(200).WithMessage("Bio must not exceed 200 characters.");

        RuleFor(command => command.ProfilePictureUrl)
            .Must(IsValidUrl).WithMessage("ProfilePictureUrl must be a valid URL.")
            .When(command => !string.IsNullOrEmpty(command.ProfilePictureUrl));
    }

    private bool IsValidUrl(string? url)
    {
        if (string.IsNullOrEmpty(url))
            return true;

        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
