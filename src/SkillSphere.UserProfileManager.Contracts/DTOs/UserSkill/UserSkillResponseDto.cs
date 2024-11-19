using SkillSphere.UserProfileManager.Core.Enums;

namespace SkillSphere.UserProfileManager.Contracts.DTOs.UserSkill;

public class UserSkillResponseDto
{
    public Guid SkillId { get; set; }

    public string SkillName { get; set; }

    public SkillLevel Level { get; set; }
}
