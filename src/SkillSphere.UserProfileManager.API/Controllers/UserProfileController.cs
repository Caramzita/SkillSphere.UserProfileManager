using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.CreateProfile;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs;
using SkillSphere.Infrastructure.Security.UserAccessor;
using Microsoft.AspNetCore.Authorization;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Queries.GetProfile;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Queries.GetAllProfiles;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.DeleteProfile;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.UpdateProfile;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.API.Controllers;

[Route("api/profiles")]
[ApiController]
[Authorize]
public class UserProfileController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IUserAccessor _userAccessor;

    public UserProfileController(IMapper mapper, IMediator mediator, IUserAccessor userAccessor)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
    }

    [HttpGet]
    public IAsyncEnumerable<UserProfile> GetAllProfiles()
    {
        var command = new GetAllProfilesQuery();
        return _mediator.CreateStream(command);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetProfileByUserId(Guid userId)
    {
        var command = new GetProfileQuery(userId);
        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromBody] UserProfileDto request)
    {
        var createCommand = _mapper.Map<CreateProfileCommand>(request);
        createCommand.UserId = GetCurrentUserId();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(createCommand);

        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UserProfileDto request)
    {
        var updateCommand = _mapper.Map<UpdateProfileCommand>(request);
        updateCommand.UserId = GetCurrentUserId();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(updateCommand);

        return result.ToActionResult();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProfile()
    {
        var userId = GetCurrentUserId();
        var command = new DeleteProfileCommand(userId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    private Guid GetCurrentUserId()
    {
        return _userAccessor.GetUserId();
    }
}
