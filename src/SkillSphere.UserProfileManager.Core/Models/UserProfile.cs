using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.Core.Models;

public class UserProfile : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public string ProfilePictureUrl { get; set; } = string.Empty;

    public string Bio { get; set; } = string.Empty;

    public List<UserSkill> Skills { get; private set; } = new List<UserSkill>();

    public List<Goal> Goals { get; private set; } = new List<Goal>();

    public List<LearningHistory> LearningHistories { get; private set; } = new List<LearningHistory>();


    public UserProfile(Guid userId, string name, string profilePictureUrl, string bio)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        ProfilePictureUrl = profilePictureUrl;
        Bio = bio;
    }

    public UserProfile(Guid id, Guid userId, string name, string profilePictureUrl, string bio,
        List<UserSkill> skills, List<Goal> goals, List<LearningHistory> learningHistories)
    {
        Id = id;
        UserId = userId;
        Name = name;
        ProfilePictureUrl = profilePictureUrl;
        Bio = bio;
        Skills = skills;
        Goals = goals;
        LearningHistories = learningHistories;
    }

    public void AddSkill(UserSkill skill)
    {
        Skills.Add(skill);
    }

    public void DeleteSkill(UserSkill skill)
    {
        Skills.Remove(Skills.FirstOrDefault(x => x.SkillId == skill.SkillId)!);
    }

    public void AddGoal(Goal goal)
    {
        Goals.Add(goal);
    }

    public void DeleteGoal(Goal goal)
    {
        Goals.Remove(Goals.FirstOrDefault(x => x.Id == goal.Id)!);
    }

    public void AddLearningHistory(LearningHistory history)
    {
        LearningHistories.Add(history);
    }

    public void DeleteLearningHistory(LearningHistory history)
    {
        LearningHistories.Remove(LearningHistories.FirstOrDefault(x => x.Id == history.Id)!);
    }
}
