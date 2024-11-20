using Microsoft.AspNetCore.Http;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands;

public interface IProfileCommand
{
    public Guid UserId { get; }

    string Name { get; }

    IFormFile? ProfilePicture { get; }

    string Bio { get; }
}