using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Queries.GetProfile;

public record GetProfileQuery(Guid UserId) : IRequest<Result<UserProfileDetailDto>>;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Result<UserProfileDetailDto>>
{
    private readonly IUserProfileRepository _userProfileRepository;

    public readonly ISkillRepository _skillRepository;

    private readonly IMapper _mapper;

    public GetProfileQueryHandler(IUserProfileRepository userProfileRepository, 
        ISkillRepository skillRepository,
        IMapper mapper)
    {
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<UserProfileDetailDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<UserProfileDetailDto>.Invalid("Профиль не найден");
        }

        var userProfileDto = _mapper.Map<UserProfileDetailDto>(userProfile);

        return Result<UserProfileDetailDto>.Success(userProfileDto);
    }
}
