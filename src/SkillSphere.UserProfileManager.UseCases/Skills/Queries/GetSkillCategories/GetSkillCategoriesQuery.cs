using MediatR;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models.Skill;
using System.Runtime.CompilerServices;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkillCategories;

public record GetSkillCategoriesQuery : IStreamRequest<SkillCategory>;

public class GetSkillCategoriesQueryHandler : IStreamRequestHandler<GetSkillCategoriesQuery, SkillCategory>
{
    private readonly ISkillRepository _skillRepository;

    public GetSkillCategoriesQueryHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
    }

    public async IAsyncEnumerable<SkillCategory> Handle(GetSkillCategoriesQuery request, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var profile in _skillRepository.GetCategoriesWithSkills())
        {
            yield return profile;
        }
    }
}
