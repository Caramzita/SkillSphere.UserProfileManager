using SkillSphere.UserProfileManager.Core.Enums;

namespace SkillSphere.UserProfileManager.Contracts.DTOs;

public class SkillDto
{
    public string Name { get; set; } = string.Empty;

    public SkillLevel Level { get; set; }
}
