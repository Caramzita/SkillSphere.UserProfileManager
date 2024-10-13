using AutoMapper;
using MediatR;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserSkill;
using SkillSphere.UserProfileManager.Core.Interfaces;
using System.Runtime.CompilerServices;

namespace SkillSphere.UserProfileManager.UseCases.UserSkills.Queries.GetUserSkills;

public record GetUserSkillsQuery(Guid UserId) : IStreamRequest<UserSkillResponseDto>;

public class GetUserSkillsQueryHandler : IStreamRequestHandler<GetUserSkillsQuery, UserSkillResponseDto>
{
    private readonly IUserSkillRepository _userSkillRepository;

    private readonly IMapper _mapper;

    public GetUserSkillsQueryHandler(IUserSkillRepository userSkillRepository, IMapper mapper)
    {
        _userSkillRepository = userSkillRepository ?? throw new ArgumentNullException(nameof(userSkillRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async IAsyncEnumerable<UserSkillResponseDto> Handle(GetUserSkillsQuery request,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var userSkill in _userSkillRepository.GetUserSkills(request.UserId))
        {
            yield return _mapper.Map<UserSkillResponseDto>(userSkill);
        }
    }
}
