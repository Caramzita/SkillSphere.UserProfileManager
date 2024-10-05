using Microsoft.EntityFrameworkCore;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.DataAccess.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly DatabaseContext _context;

    public SkillRepository(DatabaseContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IAsyncEnumerable<SkillCategory> GetCategories()
    {
        return _context.SkillCategories
            .AsNoTracking()
            .AsAsyncEnumerable();
    }

    public IAsyncEnumerable<Skill> GetCategorySkills(Guid categoryId)
    {
        return _context.Skills
            .AsNoTracking()
            .Where(s => s.CategoryId == categoryId)
            .AsAsyncEnumerable();
    }

    public async Task<SkillCategory?> GetCategoryById(Guid categoryId)
    {
        return await _context.SkillCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == categoryId);
    }

    public async Task<Skill?> GetSkillById(Guid skillId)
    {
        return await _context.Skills
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == skillId);
    }   

    public async Task AddCategory(SkillCategory category)
    {
        await _context.SkillCategories.AddAsync(category);
    }

    public async Task AddSkill(Skill skill)
    {
        await _context.Skills.AddAsync(skill);
    }

    public void DeleteCategory(SkillCategory category)
    {
        _context.SkillCategories.Remove(category);
    }

    public void DeleteSkill(Skill skill)
    {
        _context.Skills.Remove(skill);
    }    
}
