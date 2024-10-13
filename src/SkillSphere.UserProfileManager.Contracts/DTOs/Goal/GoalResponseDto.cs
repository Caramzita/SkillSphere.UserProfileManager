using SkillSphere.UserProfileManager.Core.Enums;

namespace SkillSphere.UserProfileManager.Contracts.DTOs.Goal;

public class GoalResponseDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public DateTime CreatedDate { get; init; }

    public GoalProgress Progress { get; set; }
}
