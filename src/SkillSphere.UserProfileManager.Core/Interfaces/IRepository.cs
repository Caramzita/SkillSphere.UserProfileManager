using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.Core.Interfaces;

public interface IRepository<T> where T : BaseModel
{
    Task<IEnumerable<T>> GetAllAsync(Guid userId);

    Task<T?> GetByIdAsync(Guid id, Guid userId);

    Task AddAsync(T entity);

    void UpdateAsync(T entity);

    void Delete(T entity);
}
