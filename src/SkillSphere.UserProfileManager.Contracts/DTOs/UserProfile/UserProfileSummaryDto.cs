namespace SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;

public class UserProfileSummaryDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? ProfilePictureUrl { get; set; } = string.Empty;

    public string Bio { get; set; } = string.Empty;
}
