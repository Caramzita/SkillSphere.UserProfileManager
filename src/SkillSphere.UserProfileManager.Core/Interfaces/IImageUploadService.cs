using Microsoft.AspNetCore.Http;

namespace SkillSphere.UserProfileManager.Core.Interfaces;

public interface IImageUploadService
{
    Task<string> UploadImageAsync(IFormFile imageFile);
}
