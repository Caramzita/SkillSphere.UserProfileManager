using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.Core.Interfaces;

public interface ISkillRepository
{
    IAsyncEnumerable<SkillCategory> GetCategories();

    IAsyncEnumerable<Skill> GetCategorySkills(Guid categoryId);

    Task<SkillCategory?> GetCategoryById(Guid categoryId);

    Task<List<Skill>> GetSkillsByIdsAsync(List<Guid> skillIds);

    Task<Skill?> GetSkillById(Guid skillId);

    Task AddCategory(SkillCategory category);

    void DeleteCategory(SkillCategory category);

    Task AddSkill(Skill skill);

    void DeleteSkill(Skill skill);
}
