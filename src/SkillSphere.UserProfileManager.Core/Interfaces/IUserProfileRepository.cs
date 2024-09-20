using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.Core.Interfaces;

public interface IUserProfileRepository
{
    IAsyncEnumerable<UserProfile> GetAllProfiles();

    Task<UserProfile?> GetProfileByUserId(Guid userId);

    Task AddProfile(UserProfile userProfile);

    void UpdateProfile(UserProfile profile);

    void DeleteProfile(UserProfile profile);
}
