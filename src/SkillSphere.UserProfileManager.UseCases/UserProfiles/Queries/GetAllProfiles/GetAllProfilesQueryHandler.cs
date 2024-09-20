using MediatR;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;
using System.Runtime.CompilerServices;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Queries.GetAllProfiles;

public class GetAllProfilesQueryHandler : IStreamRequestHandler<GetAllProfilesQuery, UserProfile>
{
    private readonly IUserProfileRepository _userProfileRepository;

    public GetAllProfilesQueryHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
    }

    public async IAsyncEnumerable<UserProfile> Handle(GetAllProfilesQuery request, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var profile in _userProfileRepository.GetAllProfiles())
        {
            yield return profile;
        }
    }
}
