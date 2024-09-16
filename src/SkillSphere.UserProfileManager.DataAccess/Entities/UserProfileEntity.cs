namespace SkillSphere.UserProfileManager.DataAccess.Entities;

public class UserProfileEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ProfilePictureUrl { get; set; } = string.Empty;

    public string Bio { get; set; } = string.Empty;

    public List<SkillEntity> Skills { get; set; }

    public List<GoalEntity> Goals { get; set; }

    public List<LearningHistoryEntity> LearningHistories { get; set; }
}
