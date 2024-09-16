using Microsoft.EntityFrameworkCore.Storage;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.Core.Interfaces;

public interface IGoalRepository
{
    Task<IEnumerable<Goal>> GetAllGoals(Guid userId);

    Task<Goal> GetGoalById(Guid id, Guid userId);

    Task AddGoal(Goal goal);

    Task DeleteGoal(Goal goal);

    Task UpdateGoal(Goal goal);
}
