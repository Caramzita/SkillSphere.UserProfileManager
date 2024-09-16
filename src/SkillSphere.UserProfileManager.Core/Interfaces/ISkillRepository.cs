using Microsoft.EntityFrameworkCore.Storage;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.Core.Interfaces;

public interface ISkillRepository
{
    Task<IDbContextTransaction> BeginTransactionAsync();

    Task<IEnumerable<Skill>> GetAllSkills(Guid userId);

    Task<Skill> GetSkillById(Guid id, Guid userId);

    Task AddSkill(Skill skill);

    Task DeleteSkill(Skill skill);
}
