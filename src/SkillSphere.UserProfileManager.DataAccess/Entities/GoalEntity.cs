using SkillSphere.UserProfileManager.Core.Enums;

namespace SkillSphere.UserProfileManager.DataAccess.Entities;

public class GoalEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public GoalProgress Progress { get; set; }
}
