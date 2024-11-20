using MediatR;
using Microsoft.AspNetCore.Http;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<Result<UserProfileSummaryDto>>, IProfileCommand
{
    public Guid UserId { get; set; }

    public string Name { get; } = string.Empty;

    public IFormFile? ProfilePicture { get; }

    public string Bio { get; } = string.Empty;

    public UpdateProfileCommand(string name, string bio, IFormFile? profilePicture = null)
    {
        Name = name;
        Bio = bio;
        ProfilePicture = profilePicture;
    }
}
