using SkillSphere.UserProfileManager.Core.Enums;

namespace SkillSphere.UserProfileManager.DataAccess.Entities;

public class SkillEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public SkillLevel Level { get; set; }
}
