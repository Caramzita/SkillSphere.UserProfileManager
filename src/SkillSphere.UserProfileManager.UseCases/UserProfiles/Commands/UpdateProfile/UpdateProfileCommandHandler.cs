using MediatR;
using SkillSphere.Infrastructure.Security.AuthServices;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<UserProfile>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly IAuthorizationService _authorizationService;

    public UpdateProfileCommandHandler(IUnitOfWork unitOfWork,
        IUserProfileRepository userProfileRepository,
        IAuthorizationService authorizationService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
    }

    public async Task<Result<UserProfile>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _authorizationService.UserExistsAsync(request.UserId);

        if (!userExists)
        {
            return Result<UserProfile>.Invalid("Пользователь не найден");
        }

        var existingProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (existingProfile == null)
        {
            return Result<UserProfile>.Invalid("Профиль не существует");
        }

        existingProfile.Name = request.Name;
        existingProfile.Bio = request.Bio;
        existingProfile.ProfilePictureUrl = request.ProfilePictureUrl!;

        _userProfileRepository.UpdateProfile(existingProfile);
        await _unitOfWork.CompleteAsync();

        return Result<UserProfile>.Success(existingProfile);
    }
}
