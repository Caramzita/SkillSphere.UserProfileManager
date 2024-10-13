namespace SkillSphere.UserProfileManager.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> CompleteAsync();

    Task BeginTransactionAsync();

    Task CommitAsync();

    Task RollbackAsync();
}
