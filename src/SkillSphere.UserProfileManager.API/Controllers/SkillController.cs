using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSphere.Infrastructure.Security.UserAccessor;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs;
using SkillSphere.UserProfileManager.Core.Models.Skill;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteSkill;
using SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetCategorySkills;
using SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkillById;
using SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkillCategories;

namespace SkillSphere.UserProfileManager.API.Controllers;

[Route("api/profiles/skills")]
[ApiController]
[Authorize]
public class SkillController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IUserAccessor _userAccessor;

    public SkillController(IMapper mapper, IMediator mediator, IUserAccessor userAccessor)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
    }

    [HttpGet("categories")]
    [AllowAnonymous]
    public IAsyncEnumerable<SkillCategory> GetSkillCategories()
    {
        var query = new GetSkillCategoriesQuery();

        return _mediator.CreateStream(query);
    }

    [HttpGet("categories/{categoryId:guid}/skills")]
    [AllowAnonymous]
    public IAsyncEnumerable<Skill> GetCategorySkills(Guid categoryId)
    {
        var query = new GetCategorySkillsQuery(categoryId);

        return _mediator.CreateStream(query);
    }

    [HttpGet("{skillId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSkillById(Guid skillId)
    {
        var command = new GetSkillByIdQuery(skillId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> AddSkill([FromBody] SkillDto skill)
    {
        var command = _mapper.Map<AddSkillCommand>(skill);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    [HttpDelete("{skillId:guid}")]
    public async Task<IActionResult> DeleteSkill(Guid skillId)
    {
        var userId = _userAccessor.GetUserId();
        var command = new DeleteSkillCommand(skillId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}
