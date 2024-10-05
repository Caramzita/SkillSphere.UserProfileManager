using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.Core.Interfaces;

public interface IRepository<T> where T : BaseModel
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task AddAsync(T entity);

    void UpdateAsync(T entity);

    void Delete(T entity);
}
