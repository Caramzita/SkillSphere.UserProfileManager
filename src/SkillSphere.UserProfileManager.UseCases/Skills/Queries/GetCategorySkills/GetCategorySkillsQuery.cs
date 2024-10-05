using MediatR;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models.Skill;
using System.Runtime.CompilerServices;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetCategorySkills;

public record GetCategorySkillsQuery(Guid categoryId) : IStreamRequest<Skill>;

public class GetCategorySkillsQueryHandler : IStreamRequestHandler<GetCategorySkillsQuery, Skill>
{
    private readonly ISkillRepository _skillRepository;

    public GetCategorySkillsQueryHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
    }

    public async IAsyncEnumerable<Skill> Handle(GetCategorySkillsQuery request, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var profile in _skillRepository.GetCategorySkills(request.categoryId))
        {
            yield return profile;
        }
    }
}
