using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkill;

public class GetSkillQueryHandler : IRequestHandler<GetSkillQuery, Result<Skill>>
{
    private readonly ISkillRepository _skillRepository;

    public GetSkillQueryHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
    }

    public async Task<Result<Skill>> Handle(GetSkillQuery request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetSkillById(request.Id ,request.UserId);

        if (skill == null)
        {
            return Result<Skill>.Invalid("Навык не найден.");
        }

        return Result<Skill>.Success(skill);
    }
}
