namespace SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;

public class UserProfileRequestDto
{
    public string Name { get; set; } = string.Empty;

    public string? ProfilePictureUrl { get; set; } = string.Empty;

    public string Bio { get; set; } = string.Empty;
}
