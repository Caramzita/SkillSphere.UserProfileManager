using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Queries.GetProfile;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Result<UserProfile>>
{
    private readonly IUserProfileRepository _profileRepository;

    public GetProfileQueryHandler(IUserProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<Result<UserProfile>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var profile = await _profileRepository.GetProfileByUserId(request.UserId);

        if (profile == null)
        {
            return Result<UserProfile>.Invalid("Профиль не найден");
        }

        return Result<UserProfile>.Success(profile);
    }
}
