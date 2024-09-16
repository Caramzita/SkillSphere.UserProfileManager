using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;
using SkillSphere.UserProfileManager.DataAccess.Entities;

namespace SkillSphere.UserProfileManager.DataAccess.Repositories;

public class LearningHistoryRepository : ILearningHistoryRepository
{
    private readonly DatabaseContext _context;

    private readonly IMapper _mapper;

    public LearningHistoryRepository(DatabaseContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<LearningHistory>> GetAllLearningHistory(Guid userId)
    {
        var entities = await _context.LearningHistory
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync()
            .ConfigureAwait(false);

        return _mapper.Map<IEnumerable<LearningHistory>>(entities);
    }

    public async Task<LearningHistory> GetLearningHistoryById(Guid id, Guid userId)
    {
        var entity = await _context.LearningHistory
            .AsNoTracking()
            .Where (x => x.UserId == userId)
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);

        return _mapper.Map<LearningHistory>(entity);
    }

    public async Task AddHistory(LearningHistory history)
    {
        var entity = _mapper.Map<LearningHistoryEntity>(history);

        await _context.AddAsync(entity).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteHistory(LearningHistory history)
    {
        _context.Remove(history.Id);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task UpdateHistory(LearningHistory history)
    {
        var entity = _mapper.Map<LearningHistoryEntity>(history);

        _context.Update(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}
