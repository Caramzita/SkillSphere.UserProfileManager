using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSphere.Infrastructure.Security.UserAccessor;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs;
using SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.AddHistory;
using SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.DeleteHistory;
using SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.UpdateHistoryDate;
using SkillSphere.UserProfileManager.UseCases.LearningHistories.Queries.GetHistory;

namespace SkillSphere.UserProfileManager.API.Controllers;

[Route("api/profiles/histories")]
[ApiController]
[Authorize]
public class HistoryController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IUserAccessor _userAccessor;

    public HistoryController(IMapper mapper, IMediator mediator, IUserAccessor userAccessor)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
    }

    //[HttpGet]
    //public async Task<IActionResult> GetAllHistories()
    //{
    //    var userId = _userAccessor.GetUserId();
    //    var command = new GetAllHistoryQuery(userId);

    //    var result = await _mediator.Send(command);

    //    return result.ToActionResult();
    //}

    //[HttpGet("{id:guid}")]
    //[AllowAnonymous]
    //public async Task<IActionResult> GetHistory(Guid id)
    //{
    //    var userId = _userAccessor.GetUserId();
    //    var command = new GetHistoryQuery(id, userId);

    //    var result = await _mediator.Send(command);

    //    return result.ToActionResult();
    //}

    [HttpPost]
    public async Task<IActionResult> AddHistory([FromBody] LearningHistoryDto goal)
    {
        var command = _mapper.Map<AddHistoryCommand>(goal);
        command.UserId = _userAccessor.GetUserId();

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateHistoryDate(Guid id)
    {
        var userId = _userAccessor.GetUserId();
        var command = new UpdateHistoryCommand(id, userId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteHistory(Guid id)
    {
        var userId = _userAccessor.GetUserId();
        var command = new DeleteHistoryCommand(id, userId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}
