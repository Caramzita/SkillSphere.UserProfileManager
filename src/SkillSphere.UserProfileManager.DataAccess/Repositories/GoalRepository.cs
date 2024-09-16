using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;
using SkillSphere.UserProfileManager.DataAccess.Entities;

namespace SkillSphere.UserProfileManager.DataAccess.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly DatabaseContext _context;

    private readonly IMapper _mapper;

    public GoalRepository(DatabaseContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task<IEnumerable<Goal>> GetAllGoals(Guid userId)
    {
        var entities = await _context.Goals
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync()
            .ConfigureAwait(false);

        return _mapper.Map<IEnumerable<Goal>>(entities);
    }

    public async Task<Goal> GetGoalById(Guid id, Guid userId)
    {
        var entity = await _context.Goals
            .AsNoTrackingWithIdentityResolution()
            .Where(x => x.UserId == userId)
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);

        return _mapper.Map<Goal>(entity);
    }

    public async Task AddGoal(Goal goal)
    {
        var entity = _mapper.Map<GoalEntity>(goal);

        await _context.AddAsync(entity).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
        _context.ChangeTracker.Clear();
    }

    public async Task DeleteGoal(Goal goal)
    {
        _context.Remove(goal.Id);
        await _context.SaveChangesAsync().ConfigureAwait(false);
        _context.ChangeTracker.Clear();
    }

    public async Task UpdateGoal(Goal goal)
    {
        var entity = _mapper.Map<GoalEntity>(goal);

        _context.Update(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}
