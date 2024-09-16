using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.DataAccess.Entities;

public class UserProfileEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string ProfilePictureUrl { get; set; } = string.Empty;

    public string Bio { get; set; } = string.Empty;

    public List<SkillEntity> Skills { get; set; } = new List<SkillEntity>();

    public List<GoalEntity> Goals { get; set; } = new List<GoalEntity>();

    public List<LearningHistoryEntity> LearningHistories { get; set; } = new List<LearningHistoryEntity>();
}
