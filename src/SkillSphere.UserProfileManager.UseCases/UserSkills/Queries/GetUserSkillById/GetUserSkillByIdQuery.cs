using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserSkill;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.UserSkills.Queries.GetUserSkillById;

public record GetUserSkillByIdQuery(Guid SkillId, Guid UserId) : IRequest<Result<UserSkillResponseDto>>;

public class GetUserSkillByIdQueryHandler : IRequestHandler<GetUserSkillByIdQuery, Result<UserSkillResponseDto>>
{
    private readonly IUserSkillRepository _userSkillRepository;

    private readonly IMapper _mapper;

    public GetUserSkillByIdQueryHandler(IUserSkillRepository userSkillRepository, IMapper mapper)
    {
        _userSkillRepository = userSkillRepository ?? throw new ArgumentNullException(nameof(userSkillRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<UserSkillResponseDto>> Handle(GetUserSkillByIdQuery request, CancellationToken cancellationToken)
    {
        var userSkill = await _userSkillRepository.GetUserSkillById(request.SkillId, request.UserId);

        if (userSkill == null)
        {
            return Result<UserSkillResponseDto>.Invalid("Навык не найден");
        }

        var userSkillDto = _mapper.Map<UserSkillResponseDto>(userSkill);

        return Result<UserSkillResponseDto>.Success(userSkillDto);
    }
}
