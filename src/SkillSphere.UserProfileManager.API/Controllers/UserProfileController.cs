using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.CreateProfile;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.Infrastructure.Security.UserAccessor;
using Microsoft.AspNetCore.Authorization;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Queries.GetProfile;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Queries.GetAllProfiles;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.DeleteProfile;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.UpdateProfile;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;

namespace SkillSphere.UserProfileManager.API.Controllers;

/// <summary>
/// Предоставляет Rest API для работы с профилем пользователя.
/// </summary>
[Route("api/users")]
[ApiController]
[Authorize]
public class UserProfileController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IUserAccessor _userAccessor;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserProfileController"/>.
    /// </summary>
    /// <param name="mediator"> Интерфейс для отправки команд и запросов через Mediator. </param>
    /// <param name="mapper"> Интерфейс для маппинга данных между моделями. </param>
    /// <param name="userAccessor"> Интерфейс для получения идентификатора пользователя из токена. </param>
    /// <exception cref="ArgumentNullException"> Ошибка загрузки интерфейса. </exception>
    public UserProfileController(IMapper mapper, IMediator mediator, IUserAccessor userAccessor)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
        }

    /// <summary>
    /// Получить все профиля.
    /// </summary>
    [HttpGet("profiles")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserProfileSummaryDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public IAsyncEnumerable<UserProfileSummaryDto> GetAllProfiles()
    {
        var query = new GetAllProfilesQuery();

        return _mediator.CreateStream(query);
    }

    /// <summary>
    /// Получить профиль по идентификатору пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    [HttpGet("profile/{userId:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserProfileDetailDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> GetProfileByUserId(Guid userId)
    {
        var command = new GetProfileQuery(userId);
        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Получить информацию о своем профиле.
    /// </summary>
    [HttpGet("profile")]
    [ProducesResponseType(typeof(UserProfileDetailDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = _userAccessor.GetUserId();
        var command = new GetProfileQuery(userId);
        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
    
    /// <summary>
    /// Создать профиль.
    /// </summary>
    /// <param name="request"> Модель данных профиля. </param>
    [HttpPost("profile")]
    [ProducesResponseType(typeof(UserProfileSummaryDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> CreateProfile([FromForm] UserProfileRequestDto request)
    {
        var createCommand = _mapper.Map<CreateProfileCommand>(request);
        createCommand.UserId = _userAccessor.GetUserId();

        var result = await _mediator.Send(createCommand);

        return result.ToActionResult();
    }

    /// <summary>
    /// Обновить данные профиля.
    /// </summary>
    /// <param name="request"> Модель данных профиля. </param>
    [HttpPatch("profile")]
    [ProducesResponseType(typeof(UserProfileSummaryDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> UpdateProfile([FromForm] UserProfileRequestDto request)
    {
        var updateCommand = _mapper.Map<UpdateProfileCommand>(request);
        updateCommand.UserId = _userAccessor.GetUserId();

        var result = await _mediator.Send(updateCommand);

        return result.ToActionResult();
    }

    /// <summary>
    /// Удалить профиль.
    /// </summary>
    [HttpDelete("profile")]
    [ProducesResponseType(typeof(Unit), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> DeleteProfile()
    {
        var userId = _userAccessor.GetUserId();
        var command = new DeleteProfileCommand(userId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}
