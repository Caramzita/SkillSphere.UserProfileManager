using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<Result<UserProfileSummaryDto>>, IProfileCommand
{
    public Guid UserId { get; set; }

    public string Name { get; } = string.Empty;

    public string? ProfilePictureUrl { get; } = string.Empty;

    public string Bio { get; } = string.Empty;

    public UpdateProfileCommand(string name, string bio, string? profilePictureUrl = null)
    {
        Name = name;
        Bio = bio;
        ProfilePictureUrl = profilePictureUrl;
    }
}
