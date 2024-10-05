using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSphere.Infrastructure.Security.UserAccessor;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs;
using SkillSphere.UserProfileManager.UseCases.Goals.Commands.AddGoal;
using SkillSphere.UserProfileManager.UseCases.Goals.Commands.DeleteGoal;
using SkillSphere.UserProfileManager.UseCases.Goals.Commands.UpdateGoalProgress;
using SkillSphere.UserProfileManager.UseCases.Goals.Queries.GetGoal;

namespace SkillSphere.UserProfileManager.API.Controllers;

[Route("api/profiles/goals")]
[ApiController]
[Authorize]
public class GoalController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IUserAccessor _userAccessor;

    public GoalController(IMapper mapper, IMediator mediator, IUserAccessor userAccessor)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
    }

    //[HttpGet]
    //public async Task<IActionResult> GetAllGoals()
    //{
    //    var userId = _userAccessor.GetUserId();
    //    var command = new GetAllGoalsQuery(userId);

    //    var result = await _mediator.Send(command);

    //    return result.ToActionResult();
    //}

    //[HttpGet("{id:guid}")]
    //[AllowAnonymous]
    //public async Task<IActionResult> GetGoal(Guid id)
    //{
    //    var command = new GetGoalQuery(id);

    //    var result = await _mediator.Send(command);

    //    return result.ToActionResult();
    //}

    [HttpPost]
    public async Task<IActionResult> AddGoal([FromBody] GoalDto goal)
    {
        var command = _mapper.Map<AddGoalCommand>(goal);
        command.UserId = _userAccessor.GetUserId();

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateGoalProgress(Guid id, [FromBody] UpdateGoalProgressRequest request)
    {
        var userId = _userAccessor.GetUserId();
        var command = new UpdateGoalProgressCommand(id, userId, request.Progress);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteGoal(Guid id)
    {
        var userId = _userAccessor.GetUserId();
        var command = new DeleteGoalCommand(id, userId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}
