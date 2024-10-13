using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.Core.Interfaces;

public interface IUserSkillRepository
{  
    IAsyncEnumerable<UserSkill> GetUserSkills(Guid userId);

    Task<UserSkill?> GetUserSkillById(Guid skillId, Guid userId);

    Task AddUserSkill(UserSkill userSkill);

    void RemoveUserSkill(UserSkill userSkill);
}
