namespace SkillSphere.UserProfileManager.Core.Models.Skill;

public class SkillCategory
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
