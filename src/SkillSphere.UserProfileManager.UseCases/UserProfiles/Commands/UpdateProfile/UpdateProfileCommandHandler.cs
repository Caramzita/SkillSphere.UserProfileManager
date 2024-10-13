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

    private readonly IMapper _mapper;

    public UpdateProfileCommandHandler(IUnitOfWork unitOfWork,
        IUserProfileRepository userProfileRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<UserProfileSummaryDto>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var existingProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (existingProfile == null)
        {
            return Result<UserProfileSummaryDto>.Invalid("Профиль не существует");
        }

        existingProfile.Name = request.Name;
        existingProfile.Bio = request.Bio;
        existingProfile.ProfilePictureUrl = request.ProfilePictureUrl!;

        _userProfileRepository.UpdateProfile(existingProfile);
        await _unitOfWork.CompleteAsync();

        var profileDto = _mapper.Map<UserProfileSummaryDto>(existingProfile);

        return Result<UserProfileSummaryDto>.Success(profileDto);
    }
}
