using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;

    public UnitOfWork(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public Task BeginTransactionAsync() => 
        _context.Database.BeginTransactionAsync();

    public Task CommitAsync() => 
        _context.Database.CommitTransactionAsync();

    public Task RollbackAsync() => 
        _context.Database.RollbackTransactionAsync();

    public void Dispose() => 
        _context.Dispose();
}
