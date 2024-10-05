using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Queries.GetProfile;

public record GetProfileQuery(Guid UserId) : IRequest<Result<UserProfile>>;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Result<UserProfile>>
{
    private readonly IUserProfileRepository _userProfileRepository;

    public GetProfileQueryHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
    }

    public async Task<Result<UserProfile>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var profile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (profile == null)
        {
            return Result<UserProfile>.Invalid("Профиль не найден");
        }

        return Result<UserProfile>.Success(profile);
    }
}
