using SkillSphere.UserProfileManager.Core.Enums;

namespace SkillSphere.UserProfileManager.Contracts.DTOs;

public class GoalDto
{
    public string Title { get; set; } = string.Empty;

    public GoalProgress Progress { get; set; }
}
