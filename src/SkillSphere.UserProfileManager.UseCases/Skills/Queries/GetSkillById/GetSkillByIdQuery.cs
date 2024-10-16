using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.Skill;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkillById;

public record GetSkillByIdQuery(Guid SkillId) : IRequest<Result<SkillResponseDto>>;

public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, Result<SkillResponseDto>>
{
    private readonly ISkillRepository _skillRepository;

    private readonly IMapper _mapper;

    public GetSkillByIdQueryHandler(ISkillRepository skillRepository, IMapper mapper)
    {
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<SkillResponseDto>> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetSkillById(request.SkillId);

        if (skill == null)
        {
            return Result<SkillResponseDto>.Invalid("Такой навык не найден");
        }

        var skillDto = _mapper.Map<SkillResponseDto>(skill);

        return Result<SkillResponseDto>.Success(skillDto);
    }
}
