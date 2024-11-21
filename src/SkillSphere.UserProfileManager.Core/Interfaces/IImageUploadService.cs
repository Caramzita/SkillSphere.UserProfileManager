using Microsoft.AspNetCore.Http;

namespace SkillSphere.UserProfileManager.Core.Interfaces;

public interface IImageUploadService
{
    Task<string> UploadImage(IFormFile imageFile);

    Task DeleteImage(string profilePictureUrl);
}
