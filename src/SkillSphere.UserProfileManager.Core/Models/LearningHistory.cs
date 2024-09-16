namespace SkillSphere.UserProfileManager.Core.Models;

public class LearningHistory
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public string CourseTitle { get; set; } = string.Empty;

    public DateTime CompletedDate { get; private set; }

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; init; }

    public LearningHistory(Guid userId, string courseTitle, string description)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        CourseTitle = courseTitle;
        Description = description;
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

    public bool CanEditOrDelete()
    {
        return CreatedAt.AddHours(24) > DateTime.UtcNow;
    }
}
