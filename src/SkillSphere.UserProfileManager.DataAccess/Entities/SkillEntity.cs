using SkillSphere.UserProfileManager.Core.Enums;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.DataAccess.Entities;

public class SkillEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public SkillLevel Level { get; set; }
}
