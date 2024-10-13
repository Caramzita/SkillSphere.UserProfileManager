using SkillSphere.UserProfileManager.Core.Enums;

namespace SkillSphere.UserProfileManager.Core.Models.Skill;

public class UserSkill
{
    public Guid UserId { get; init; }

    public Guid SkillId { get; set; }

    public SkillLevel Level { get; private set; }

    public Skill Skill { get; set; } = null!;

    public UserSkill(Guid userId, Guid skillId)
    {
        UserId = userId;
        SkillId = skillId;
        Level = SkillLevel.Beginner;
    }

    public UserSkill(Guid userId, Guid skillId, SkillLevel level)
    {
        UserId = userId;
        SkillId = skillId;
        Level = level;
    }

    public void ChangeLevel(SkillLevel level)
    {
        Level = level;
    }
}
