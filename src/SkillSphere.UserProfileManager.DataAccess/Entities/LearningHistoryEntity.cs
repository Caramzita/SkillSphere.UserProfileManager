using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.DataAccess.Entities;

public class LearningHistoryEntity : BaseEntity
{
    public string CourseTitle { get; set; } = string.Empty;

    public DateTime CompletedDate { get; set; }

    public string Description { get; set; } = string.Empty;
}
