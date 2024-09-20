namespace SkillSphere.UserProfileManager.Core.Models;

public class LearningHistory : BaseModel
{
    public string CourseTitle { get; private set; } = string.Empty;

    public DateTime CompletedDate { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; init; }

    public LearningHistory(Guid userId, string courseTitle, string description, DateTime completedDate)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        CourseTitle = courseTitle;
        Description = description;
        CompletedDate = completedDate;
        CreatedAt = DateTime.UtcNow;
    }

    public LearningHistory(Guid id, Guid userId, string courseTitle,
        DateTime completedDate, string description, DateTime createdAt)
    {
        Id = id;
        UserId = userId;
        CourseTitle = courseTitle;
        CompletedDate = completedDate;
        Description = description;
        CreatedAt = createdAt;
    }

    public void CompleteCourse()
    {
        CompletedDate = DateTime.UtcNow;
    }
}
