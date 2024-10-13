using Microsoft.EntityFrameworkCore;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.DataAccess.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly DatabaseContext _context;

    public UserProfileRepository(DatabaseContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IAsyncEnumerable<UserProfile> GetAllProfiles()
    {
        return _context.UserProfiles
            .AsNoTracking()
            .AsAsyncEnumerable();
    }

    public async Task<UserProfile?> GetProfileByUserId(Guid userId)
    {
        return await _context.UserProfiles
            .AsNoTracking()
            .Include(x => x.Goals)
            .Include(x => x.Skills)
                .ThenInclude(us => us.Skill)
                .ThenInclude(s => s.Category)
            .Include(x => x.LearningHistories)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.UserId == userId)
            .ConfigureAwait(false);
    }

    public async Task AddProfile(UserProfile profile)
    {
        await _context.AddAsync(profile).ConfigureAwait(false);
    }

    public void DeleteProfile(UserProfile profile)
    {
        _context.Remove(profile);
    }

    public void UpdateProfile(UserProfile profile)
    {
        _context.Attach(profile);
    }
}