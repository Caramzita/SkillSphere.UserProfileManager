using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;
using SkillSphere.UserProfileManager.DataAccess.Entities;

namespace SkillSphere.UserProfileManager.DataAccess.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly IMapper _mapper;

    private readonly DatabaseContext _context;

    public UserProfileRepository(IMapper mapper, DatabaseContext context)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async IAsyncEnumerable<UserProfile> GetAllProfiles()
    {
        var entities = _context.UserProfiles
        .Include(x => x.Goals)
        .Include(x => x.Skills)
        .Include(x => x.LearningHistories)
        .AsNoTracking()
        .AsSplitQuery()
        .AsAsyncEnumerable();

        await foreach (var entity in entities)
        {
            yield return _mapper.Map<UserProfile>(entity);
        }
    }

    public async Task<UserProfile?> GetProfileByUserId(Guid userId)
    {
        var entity = await _context.UserProfiles
            .AsNoTracking()
            .Include(x => x.Goals)
            .Include(x => x.Skills)
            .Include(x => x.LearningHistories)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.UserId == userId)
            .ConfigureAwait(false);

        return _mapper.Map<UserProfile>(entity);
    }

    public async Task AddProfile(UserProfile profile)
    {
        var entity = _mapper.Map<UserProfileEntity>(profile);

        await _context.AddAsync(entity).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteProfile(UserProfile profile)
    {
        var entity = _mapper.Map<UserProfileEntity>(profile);

        _context.Remove(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task UpdateProfile(UserProfile profile)
    {
        var entity = _mapper.Map<UserProfileEntity>(profile);

        _context.Update(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}