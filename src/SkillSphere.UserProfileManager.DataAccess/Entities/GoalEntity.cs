using SkillSphere.UserProfileManager.Core.Enums;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.DataAccess.Entities;

public class GoalEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public GoalProgress Progress { get; set; }
}
