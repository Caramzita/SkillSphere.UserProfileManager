using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<UserProfileSummaryDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly IImageUploadService _imageUploadService;

    private readonly IMapper _mapper;

    public UpdateProfileCommandHandler(IUnitOfWork unitOfWork,
        IUserProfileRepository userProfileRepository,
        IImageUploadService imageUploadService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _imageUploadService = imageUploadService ?? throw new ArgumentNullException(nameof(imageUploadService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<UserProfileSummaryDto>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var existingProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (existingProfile == null)
        {
            return Result<UserProfileSummaryDto>.Invalid("Профиль не существует");
        }

        var imageUrl = string.Empty;

        if (request.ProfilePicture != null)
        {
            imageUrl = await _imageUploadService.UploadImageAsync(request.ProfilePicture);
        }

        existingProfile.Name = request.Name;
        existingProfile.Bio = request.Bio;
        existingProfile.ProfilePictureUrl = imageUrl;

        _userProfileRepository.UpdateProfile(existingProfile);
        await _unitOfWork.CompleteAsync();

        var profileDto = _mapper.Map<UserProfileSummaryDto>(existingProfile);

        return Result<UserProfileSummaryDto>.Success(profileDto);
    }
}
