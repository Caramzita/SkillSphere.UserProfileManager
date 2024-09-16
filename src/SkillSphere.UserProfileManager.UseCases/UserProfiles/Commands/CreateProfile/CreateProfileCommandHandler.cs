using MediatR;
using SkillSphere.Infrastructure.Security.AuthServices;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.CreateProfile;

public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Result<UserProfile>>
{
    private readonly IUserProfileRepository _profileRepository;

    private readonly IAuthorizationService _authorizationService;

    public CreateProfileCommandHandler(
        IUserProfileRepository profileRepository,
        IAuthorizationService authorizationService)
    {
        _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
        _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
    }

    public async Task<Result<UserProfile>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _authorizationService.UserExistsAsync(request.UserId);

        if (!userExists)
        {
            return Result<UserProfile>.Invalid("Пользователь не найден");
        }

        var existingProfile = await _profileRepository.GetProfileByUserId(request.UserId);

        if (existingProfile != null)
        {
            return Result<UserProfile>.Invalid("Профиль уже существует");
        }

        var newProfile = new UserProfile(
            request.UserId,
            request.Name,
            request.ProfilePictureUrl,
            request.Bio
        );

        await _profileRepository.AddProfile(newProfile);

        return Result<UserProfile>.Success(newProfile);
    }
}
