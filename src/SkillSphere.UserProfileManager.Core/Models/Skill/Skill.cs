namespace SkillSphere.UserProfileManager.Core.Models.Skill;

public class Skill
{
    public Guid Id { get; init; }

    public string Name { get; private set; } = string.Empty;

    public Guid CategoryId { get; init; }

    public Skill(string name, Guid categoryId)
    {
        Id = Guid.NewGuid();
        CategoryId = categoryId;
        Name = name;
    }

    public Skill(Guid id, Guid categoryId, string name)
    {
        Id = id;
        CategoryId = categoryId;
        Name = name;
    }
}
