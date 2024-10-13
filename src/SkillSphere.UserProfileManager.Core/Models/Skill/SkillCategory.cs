namespace SkillSphere.UserProfileManager.Core.Models.Skill;

public class SkillCategory
{
    public Guid Id { get; init; }

    public string Name { get; private set; } = string.Empty;

    public SkillCategory(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public SkillCategory(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
