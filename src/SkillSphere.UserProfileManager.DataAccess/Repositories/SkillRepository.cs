using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;
using SkillSphere.UserProfileManager.DataAccess.Entities;

namespace SkillSphere.UserProfileManager.DataAccess.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly DatabaseContext _context;

    private readonly IMapper _mapper;

    public SkillRepository(DatabaseContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<Skill>> GetAllSkills(Guid userId)
    {
        var entities = await _context.Skills
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync()
            .ConfigureAwait(false);

        return _mapper.Map<IEnumerable<Skill>>(entities);
    }

    public async Task<Skill> GetSkillById(Guid id, Guid userId)
    {
        var entity = await _context.Skills
            .AsNoTracking()
            .Where (x => x.UserId == userId)
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);

        return _mapper.Map<Skill>(entity);
    }

    public async Task AddSkill(Skill skill)
    {
        var entity = _mapper.Map<SkillEntity>(skill);

        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
    }

    public async Task DeleteSkill(Skill skill)
    {
        var entity = _mapper.Map<SkillEntity>(skill);

        _context.Remove(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
        _context.ChangeTracker.Clear();
    }
}
