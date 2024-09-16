using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.Core.Interfaces;

public interface IUserProfileRepository
{
    IAsyncEnumerable<UserProfile> GetAllProfiles();

    Task<UserProfile?> GetProfileByUserId(Guid userId);

    Task AddProfile(UserProfile userProfile);

    Task UpdateProfile(UserProfile profile);

    Task DeleteProfile(UserProfile profile);
}
