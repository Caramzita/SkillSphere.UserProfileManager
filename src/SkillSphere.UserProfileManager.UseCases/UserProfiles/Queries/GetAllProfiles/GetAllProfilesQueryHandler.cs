using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;
using System.Runtime.CompilerServices;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Queries.GetAllProfiles;

public class GetAllProfilesQueryHandler : IStreamRequestHandler<GetAllProfilesQuery, UserProfile>
{
    private readonly IUserProfileRepository _profileRepository;

    public GetAllProfilesQueryHandler(IUserProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async IAsyncEnumerable<UserProfile> Handle(GetAllProfilesQuery request, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var profile in _profileRepository.GetAllProfiles())
        {
            yield return profile;
        }
    }
}
