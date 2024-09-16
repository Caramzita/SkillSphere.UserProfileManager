using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.Core.Interfaces;

public interface ILearningHistoryRepository
{
    Task<IEnumerable<LearningHistory>> GetAllLearningHistory(Guid userId);

    Task<LearningHistory> GetLearningHistoryById(Guid id, Guid userId);

    Task AddHistory(LearningHistory history);

    Task UpdateHistory(LearningHistory history);

    Task DeleteHistory(LearningHistory history);
}
