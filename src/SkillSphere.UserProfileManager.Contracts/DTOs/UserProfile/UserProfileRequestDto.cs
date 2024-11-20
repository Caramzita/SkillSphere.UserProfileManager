using Microsoft.AspNetCore.Http;

namespace SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;

public class UserProfileRequestDto
{
    public string Name { get; set; } = string.Empty;

    public IFormFile? ProfilePicture { get; set; }

    public string Bio { get; set; } = string.Empty;
}
