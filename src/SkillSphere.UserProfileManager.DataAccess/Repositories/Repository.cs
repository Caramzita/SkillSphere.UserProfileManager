using Microsoft.EntityFrameworkCore;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : BaseModel
{
    private readonly DatabaseContext _context;

    public Repository(DatabaseContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<T>> GetAllAsync(Guid userId)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>()
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void UpdateAsync(T entity)
    {
        _context.Set<T>().Attach(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }    
}
