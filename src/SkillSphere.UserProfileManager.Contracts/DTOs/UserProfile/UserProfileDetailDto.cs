using SkillSphere.UserProfileManager.Contracts.DTOs.Goal;
using SkillSphere.UserProfileManager.Contracts.DTOs.LearningHistory;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserSkill;

namespace SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;

public class UserProfileDetailDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? ProfilePictureUrl { get; set; } = string.Empty;

    public string Bio { get; set; } = string.Empty;

    public List<UserSkillResponseDto> Skills { get; set; } = new();

    public List<GoalResponseDto> Goals { get; set; } = new();

    public List<LearningHistoryResponseDto> LearningHistories { get; set; } = new();
}
