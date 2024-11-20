using MediatR;
using Microsoft.AspNetCore.Hosting;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.DeleteProfile;

public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly string _uploadsFolderPath;

    public DeleteProfileCommandHandler(IUnitOfWork unitOfWork, 
        IUserProfileRepository userProfileRepository, 
        IWebHostEnvironment environment)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _uploadsFolderPath = Path.Combine(environment.ContentRootPath, @"..\..\..\SkillSphere.Files");
    }

    public async Task<Result<Unit>> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (profile == null)
        {
            return Result<Unit>.Invalid("Профиль не существует");
        }

        if (!string.IsNullOrEmpty(profile.ProfilePictureUrl))
        {
            var fileName = Path.GetFileName(profile.ProfilePictureUrl);
            var avatarPath = Path.Combine(_uploadsFolderPath, fileName);

            try
            {
                if (File.Exists(avatarPath))
                {
                    File.Delete(avatarPath);
                }
            }
            catch (Exception)
            {
                return Result<Unit>.Invalid("Ошибка при удалении изображения");
            }
        }

        _userProfileRepository.DeleteProfile(profile);
        await _unitOfWork.CompleteAsync();

        return Result<Unit>.Empty();
    }
}