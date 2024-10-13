using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSphere.Infrastructure.Security.UserAccessor;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.Goal;
using SkillSphere.UserProfileManager.UseCases.Goals.Commands.AddGoal;
using SkillSphere.UserProfileManager.UseCases.Goals.Commands.DeleteGoal;
using SkillSphere.UserProfileManager.UseCases.Goals.Commands.UpdateGoalProgress;
using SkillSphere.UserProfileManager.UseCases.Goals.Queries.GetAllGoals;

namespace SkillSphere.UserProfileManager.API.Controllers;

/// <summary>
/// Предоставляет Rest API для работы с целями.
/// </summary>
[Route("api/users/profile/goals")]
[ApiController]
[Authorize]
public class GoalController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IUserAccessor _userAccessor;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="GoalController"/>.
    /// </summary>
    /// <param name="mediator"> Интерфейс для отправки команд и запросов через Mediator. </param>
    /// <param name="mapper"> Интерфейс для маппинга данных между моделями. </param>
    /// <param name="userAccessor"> Интерфейс для получения идентификатора пользователя из токена. </param>
    /// <exception cref="ArgumentNullException"> Ошибка загрузки интерфейса. </exception>
    public GoalController(IMapper mapper, IMediator mediator, IUserAccessor userAccessor)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
    }

    /// <summary>
    /// Получить все цели пользователя.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(GoalResponseDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> GetAllUserGoals()
    {
        var userId = _userAccessor.GetUserId();
        var command = new GetAllGoalsQuery(userId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Добавить цель.
    /// </summary>
    /// <param name="goal"> Модель данных цели </param>
    [HttpPost]
    [ProducesResponseType(typeof(GoalResponseDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> AddGoal([FromBody] GoalRequestDto goal)
    {
        var command = _mapper.Map<AddGoalCommand>(goal);
        command.UserId = _userAccessor.GetUserId();

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Обновить прогресс цели.
    /// </summary>
    /// <param name="id"> идентификатор цели. </param>
    /// <param name="request"> Модель данных обновления прогресса. </param>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(Unit), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> UpdateGoalProgress(Guid id, [FromBody] UpdateGoalProgressRequest request)
    {
        var userId = _userAccessor.GetUserId();
        var command = new UpdateGoalProgressCommand(id, userId, request.Progress);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
    
    /// <summary>
    /// Удалить цель.
    /// </summary>
    /// <param name="id"> Идентификатор цели. </param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Unit), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> DeleteGoal(Guid id)
    {
        var userId = _userAccessor.GetUserId();
        var command = new DeleteGoalCommand(id, userId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}
