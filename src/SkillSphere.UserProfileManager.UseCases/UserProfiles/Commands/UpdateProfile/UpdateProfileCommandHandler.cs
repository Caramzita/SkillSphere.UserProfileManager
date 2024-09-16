using MediatR;
using SkillSphere.Infrastructure.Security.AuthServices;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<UserProfile>>
{
    private readonly IUserProfileRepository _profileRepository;

    private readonly IAuthorizationService _authorizationService;

    public UpdateProfileCommandHandler(IUserProfileRepository profileRepository,
        IAuthorizationService authorizationService)
    {
        _profileRepository = profileRepository;
        _authorizationService = authorizationService;
    }

    public async Task<Result<UserProfile>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _authorizationService.UserExistsAsync(request.UserId);

        if (!userExists)
        {
            return Result<UserProfile>.Invalid("Пользователь не найден");
        }

        var existingProfile = await _profileRepository.GetProfileByUserId(request.UserId);

        if (existingProfile == null)
        {
            return Result<UserProfile>.Invalid("Профиль не существует");
        }

        existingProfile.Name = request.Name;
        existingProfile.Bio = request.Bio;
        existingProfile.ProfilePictureUrl = request.ProfilePictureUrl;

        await _profileRepository.UpdateProfile(existingProfile);

        return Result<UserProfile>.Success(existingProfile);
    }
}
