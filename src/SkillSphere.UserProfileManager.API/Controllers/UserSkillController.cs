using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSphere.Infrastructure.Security.UserAccessor;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserSkill;
using SkillSphere.UserProfileManager.UseCases.UserSkills.Commands.AddUserSkill;
using SkillSphere.UserProfileManager.UseCases.UserSkills.Commands.DeleteUserSkill;
using SkillSphere.UserProfileManager.UseCases.UserSkills.Queries.GetUserSkillById;
using SkillSphere.UserProfileManager.UseCases.UserSkills.Queries.GetUserSkills;

namespace SkillSphere.UserProfileManager.API.Controllers;

/// <summary>
/// Предоставляет Rest API для работы с навыками пользователя.
/// </summary>
[Route("api/users/profile/skills")]
[ApiController]
[Authorize]
public class UserSkillController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly IUserAccessor _userAccessor;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserSkillController"/>.
    /// </summary>
    /// <param name="mediator"> Интерфейс для отправки команд и запросов через Mediator. </param>
    /// <param name="userAccessor"> Интерфейс для получения идентификатора пользователя из токена. </param>
    /// <exception cref="ArgumentNullException"> Ошибка загрузки интерфейса. </exception>
    public UserSkillController(IMediator mediator, IUserAccessor userAccessor)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
    }

    /// <summary>
    /// Получить навыки пользователя.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(UserSkillResponseDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public IAsyncEnumerable<UserSkillResponseDto> GetUserSkills()
    {
        var userId = _userAccessor.GetUserId();
        var query = new GetUserSkillsQuery(userId);

        return _mediator.CreateStream(query);
    }

    /// <summary>
    /// Получить навык пользователя по идентификатору.
    /// </summary>
    /// <param name="skillId"> Идентификатор навыка. </param>
    [HttpGet("{skillId:guid}")]
    [ProducesResponseType(typeof(UserSkillResponseDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> GetUserSkillById(Guid skillId)
    {
        var userId = _userAccessor.GetUserId();
        var query = new GetUserSkillByIdQuery(skillId, userId);

        var result = await _mediator.Send(query);

        return result.ToActionResult();
    }

    /// <summary>
    /// Добавить навык пользователю.
    /// </summary>
    /// <param name="request"> Модель данных навыка пользователя. </param>
    [HttpPost]
    [ProducesResponseType(typeof(UserSkillResponseDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> AddUserSkill([FromBody] UserSkillRequestDto request)
    {
        var userId = _userAccessor.GetUserId();
        var command = new AddUserSkillCommand(userId, request.SkillId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Удалить навыка пользователя.
    /// </summary>
    /// <param name="skillId"> Идентификатор навыка. </param>
    [HttpDelete("{skillId:guid}")]
    [ProducesResponseType(typeof(Unit), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> RemoveUserSkill(Guid skillId)
    {
        var userId = _userAccessor.GetUserId();
        var command = new DeleteUserSkillCommand(userId, skillId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}
