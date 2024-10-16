using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.Skill;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkillsByIds;

public record GetSkillsByIdsQuery(List<Guid> SkillIds) : IRequest<Result<List<SkillsListRequestDto>>>;

public class GetSkillsByIdsQueryHandler : IRequestHandler<GetSkillsByIdsQuery, Result<List<SkillsListRequestDto>>>
{
    private readonly ISkillRepository _skillRepository;

    private readonly IMapper _mapper;

    public GetSkillsByIdsQueryHandler(ISkillRepository skillRepository, IMapper mapper)
    {
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<List<SkillsListRequestDto>>> Handle(GetSkillsByIdsQuery request, CancellationToken cancellationToken)
    {
        var skills = await _skillRepository.GetSkillsByIdsAsync(request.SkillIds);

        if (skills == null || skills.Count != request.SkillIds.Count)
        {
            return Result<List<SkillsListRequestDto>>.Invalid("Some skills were not found.");
        }

        var skillsDto = _mapper.Map<List<SkillsListRequestDto>>(skills);

        return Result<List<SkillsListRequestDto>>.Success(skillsDto);
    }
}