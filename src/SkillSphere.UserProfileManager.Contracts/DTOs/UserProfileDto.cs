namespace SkillSphere.UserProfileManager.Contracts.DTOs;

public class UserProfileDto
{
    public string Name { get; set; } = string.Empty;

    public string? ProfilePictureUrl { get; set; } = string.Empty;

    public string Bio { get; set; } = string.Empty;
}
