namespace SkillSphere.UserProfileManager.Core.Models.Skill;

public class SkillCategory
{
    public Guid Id { get; init; }

    public string Name { get; private set; } = string.Empty;

    public ICollection<Skill> Skills { get; private set; } = new List<Skill>();

    public SkillCategory(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public SkillCategory(Guid id, string name, ICollection<Skill> skills)
    {
        Id = id;
        Name = name;
        Skills = skills;
    }

    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }

    public void RemoveSkill(Skill skill) 
    {
        Skills.Remove(skill); 
    }
}