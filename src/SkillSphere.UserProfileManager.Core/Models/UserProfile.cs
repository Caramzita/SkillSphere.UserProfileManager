namespace SkillSphere.UserProfileManager.Core.Models;

public class UserProfile : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public string ProfilePictureUrl { get; set; } = string.Empty;

    public string Bio { get; set; } = string.Empty;

    public List<Skill> Skills { get; private set; } = new List<Skill>();

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
        List<Skill> skills, List<Goal> goals, List<LearningHistory> learningHistories)
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

    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }

    public void DeleteSkill(Skill skill)
    {
        Skills.Remove(Skills.FirstOrDefault(x => x.Id == skill.Id)!);
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
