using Microsoft.EntityFrameworkCore;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.DataAccess.Repositories;

public class UserSkillRepository : IUserSkillRepository
{
    private readonly DatabaseContext _context;

    public UserSkillRepository(DatabaseContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IAsyncEnumerable<UserSkill> GetUserSkills(Guid userId)
    {
        return _context.UserSkills
            .AsNoTracking()
            .Where(us => us.UserId == userId)
            .AsSplitQuery()
            .AsAsyncEnumerable();
    }

    public async Task<UserSkill?> GetUserSkillById(Guid skillId, Guid userId)
    {
        return await _context.UserSkills
            .AsNoTracking()
            .Where(ud => ud.UserId == userId)
            .FirstOrDefaultAsync(us => us.SkillId == skillId);
    }

    public async Task AddUserSkill(UserSkill userSkill)
    {
        await _context.AddAsync(userSkill);
    }   

    public void RemoveUserSkill(UserSkill userSkill)
    {
        _context.Remove(userSkill);
    }
}
