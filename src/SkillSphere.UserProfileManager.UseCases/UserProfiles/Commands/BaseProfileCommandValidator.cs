using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands;

public class BaseProfileCommandValidator<T> : AbstractValidator<T>
    where T : IProfileCommand
{
    private readonly string[] _validImageExtensions = { ".jpg", ".jpeg", ".png", ".bmp" };

    protected BaseProfileCommandValidator()
    {
        RuleFor(command => command.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

        RuleFor(command => command.Bio)
            .MaximumLength(200).WithMessage("Bio must not exceed 200 characters.");

        RuleFor(command => command.ProfilePicture)
           .Must(BeAValidImage)
           .WithMessage("Profile picture must be a valid image (jpg, jpeg, png, bmp).")
           .When(command => command.ProfilePicture != null);
    }

    private bool BeAValidImage(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return _validImageExtensions.Contains(extension);
    }
}
