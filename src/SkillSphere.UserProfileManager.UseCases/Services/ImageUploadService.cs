using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.Services;

public class ImageUploadService : IImageUploadService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly string _uploadsFolderPath;

    public ImageUploadService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _uploadsFolderPath = configuration["UploadsFolderPath"];
    }

    public async Task<string> UploadImage(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty.", nameof(file));
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_uploadsFolderPath, uniqueFileName);

            if (!Directory.Exists(_uploadsFolderPath))
            {
                Directory.CreateDirectory(_uploadsFolderPath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var request = _httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}/uploads/{uniqueFileName}";
        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred while saving the image: " + ex.Message, ex);
        }
    }

    public async Task DeleteImage(string profilePictureUrl)
    {
        try
        {
            var fileName = Path.GetFileName(profilePictureUrl);
            var avatarPath = Path.Combine(_uploadsFolderPath, fileName);

            if (File.Exists(avatarPath))
            {
                File.Delete(avatarPath);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred while deleted the image: " + ex.Message, ex);
        }
    }
}