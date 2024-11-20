using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.Services;

public class ImageUploadService : IImageUploadService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWebHostEnvironment _environment;

    public ImageUploadService(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
    {
        _httpContextAccessor = httpContextAccessor;
        _environment = environment;
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        var uploadsFolderPath = Path.Combine(_environment.ContentRootPath, @"..\..\..\SkillSphere.Files");

        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
        var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

        if (!Directory.Exists(uploadsFolderPath))
        {
            Directory.CreateDirectory(uploadsFolderPath);
        }

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        var request = _httpContextAccessor.HttpContext.Request;
        return $"{request.Scheme}://{request.Host}/SkillSphere.Files/{uniqueFileName}";
    }
}
