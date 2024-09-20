using SkillSphere.UserProfileManager.Core.Enums;

namespace SkillSphere.UserProfileManager.Core.Models;

public class Goal : BaseModel
{
    public string Title { get; set; } = string.Empty;

    public DateTime CreatedDate { get; init; }

    public GoalProgress Progress { get; set; }

    public Goal(Guid userId, string title)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Title = title;
        CreatedDate = DateTime.UtcNow;
        Progress = GoalProgress.NotStarted;
    }

    public Goal(Guid id, Guid userId, string title, DateTime createdDate, GoalProgress progress)
    {
        Id = id;
        UserId = userId;
        Title = title;
        CreatedDate = createdDate;
        Progress = progress;
    }
}
