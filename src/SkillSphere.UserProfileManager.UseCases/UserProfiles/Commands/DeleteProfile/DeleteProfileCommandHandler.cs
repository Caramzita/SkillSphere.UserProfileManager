using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.DeleteProfile;

public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly IImageUploadService _imageUploadService;

    public DeleteProfileCommandHandler(IUnitOfWork unitOfWork, 
        IUserProfileRepository userProfileRepository,
        IImageUploadService imageUploadService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _imageUploadService = imageUploadService ?? throw new ArgumentNullException(nameof(imageUploadService));
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
            await _imageUploadService.DeleteImage(profile.ProfilePictureUrl);
        }

        _userProfileRepository.DeleteProfile(profile);
        await _unitOfWork.CompleteAsync();

        return Result<Unit>.Empty();
    }
}