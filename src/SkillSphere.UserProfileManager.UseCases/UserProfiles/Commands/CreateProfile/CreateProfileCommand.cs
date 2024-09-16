using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.CreateProfile;

public class CreateProfileCommand : IRequest<Result<UserProfile>>
{
    public Guid UserId { get; set; }

    public string Name { get; } = string.Empty;

    public string? ProfilePictureUrl { get; } = string.Empty;

    public string Bio { get; } = string.Empty;

    public CreateProfileCommand(string name, string bio, string? profilePictureUrl = null)
    {
        Name = name;
        Bio = bio;
        ProfilePictureUrl = profilePictureUrl;
    }
}