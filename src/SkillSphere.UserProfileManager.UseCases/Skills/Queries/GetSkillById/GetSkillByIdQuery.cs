using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkillById;

public record GetSkillByIdQuery(Guid skillId) : IRequest<Result<Skill>>;

public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, Result<Skill>>
{
    private readonly ISkillRepository _skillRepository;

    public GetSkillByIdQueryHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
    }

    public async Task<Result<Skill>> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetSkillById(request.skillId);

        if (skill == null)
        {
            return Result<Skill>.Invalid("Такой навык не найден");
        }

        return Result<Skill>.Success(skill);
    }
}
