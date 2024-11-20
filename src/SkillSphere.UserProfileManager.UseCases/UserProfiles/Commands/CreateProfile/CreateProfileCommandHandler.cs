using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SkillSphere.Infrastructure.Security.AuthServices;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.CreateProfile;

public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Result<UserProfileSummaryDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly IAuthorizationService _authorizationService;

    private readonly IImageUploadService _imageUploadService;

    private readonly IMapper _mapper;

    public CreateProfileCommandHandler(IUserProfileRepository userProfileRepository,
        IUnitOfWork unitOfWork,
        IAuthorizationService authorizationService,
        IImageUploadService imageUploadService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        _imageUploadService = imageUploadService ?? throw new ArgumentNullException(nameof(imageUploadService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<UserProfileSummaryDto>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _authorizationService.UserExistsAsync(request.UserId);

        if (!userExists)
        {
            return Result<UserProfileSummaryDto>.Invalid("Пользователь не найден");
        }

        var existingProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (existingProfile != null)
        {
            return Result<UserProfileSummaryDto>.Invalid("Профиль уже существует");
        }

        var imageUrl = string.Empty;

        if (request.ProfilePicture != null)
        {
            imageUrl = await _imageUploadService.UploadImageAsync(request.ProfilePicture);
        }

        var newProfile = new UserProfile(
            request.UserId,
            request.Name,
            imageUrl,
            request.Bio
        );

        await _userProfileRepository.AddProfile(newProfile);
        await _unitOfWork.CompleteAsync();

        var newProfileDto = _mapper.Map<UserProfileSummaryDto>(newProfile);

        return Result<UserProfileSummaryDto>.Success(newProfileDto);
    }
}
