using AutoMapper;
using MediatR;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;
using SkillSphere.UserProfileManager.Core.Interfaces;
using System.Runtime.CompilerServices;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Queries.GetAllProfiles;

public record GetAllProfilesQuery : IStreamRequest<UserProfileSummaryDto>;

public class GetAllProfilesQueryHandler : IStreamRequestHandler<GetAllProfilesQuery, UserProfileSummaryDto>
{
    private readonly IUserProfileRepository _userProfileRepository;

    private readonly IMapper _mapper;

    public GetAllProfilesQueryHandler(IUserProfileRepository userProfileRepository, IMapper mapper)
    {
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async IAsyncEnumerable<UserProfileSummaryDto> Handle(GetAllProfilesQuery request, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var profile in _userProfileRepository.GetAllProfiles())
        {
            yield return _mapper.Map<UserProfileSummaryDto>(profile);
        }
    }
}
