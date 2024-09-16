namespace SkillSphere.UserProfileManager.DataAccess.Entities;

public class LearningHistoryEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string CourseTitle { get; set; } = string.Empty;

    public DateTime CompletedDate { get; set; }

    public string Description { get; set; } = string.Empty;
}
