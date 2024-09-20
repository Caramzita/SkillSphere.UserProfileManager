﻿using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetAllSkills;

public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, Result<IEnumerable<Skill>>>
{
    private readonly IRepository<Skill> _skillRepository;

    public GetAllSkillsQueryHandler(IRepository<Skill> skillRepository)
    {
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
    }

    public async Task<Result<IEnumerable<Skill>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await _skillRepository.GetAllAsync(request.UserId);

        if (skills == null || !skills.Any())
        {
            return Result<IEnumerable<Skill>>.Invalid("Навыки не найдены.");
        }

        return Result<IEnumerable<Skill>>.Success(skills);
    }
}
