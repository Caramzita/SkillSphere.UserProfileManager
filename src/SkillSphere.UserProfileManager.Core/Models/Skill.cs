using SkillSphere.UserProfileManager.Core.Enums;

namespace SkillSphere.UserProfileManager.Core.Models;

public class Skill
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public string Name { get; set; } = string.Empty;

    public SkillLevel Level { get; set; }

    public Skill(Guid userId, string name, SkillLevel level)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Level = level;
    }

    public Skill(Guid id, Guid userId, string name, SkillLevel level)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Level = level;
    }
}
