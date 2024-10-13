using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSphere.Infrastructure.Security.UserAccessor;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.LearningHistory;
using SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.AddHistory;
using SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.DeleteHistory;
using SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.UpdateHistoryDate;
using SkillSphere.UserProfileManager.UseCases.LearningHistories.Queries.GetAllHistory;

namespace SkillSphere.UserProfileManager.API.Controllers;

/// <summary>
/// Предоставляет Rest API для работы с историей обучения.
/// </summary>
[Route("api/users/profile/histories")]
[ApiController]
[Authorize]
public class HistoryController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IUserAccessor _userAccessor;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="HistoryController"/>.
    /// </summary>
    /// <param name="mediator"> Интерфейс для отправки команд и запросов через Mediator. </param>
    /// <param name="mapper"> Интерфейс для маппинга данных между моделями. </param>
    /// <param name="userAccessor"> Интерфейс для получения идентификатора пользователя из токена. </param>
    /// <exception cref="ArgumentNullException"> Ошибка загрузки интерфейса. </exception>
    public HistoryController(IMapper mapper, IMediator mediator, IUserAccessor userAccessor)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
    }

    /// <summary>
    /// Получить историю обучения пользователя.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(LearningHistoryResponseDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> GetAllUserHistories()
    {
        var userId = _userAccessor.GetUserId();
        var command = new GetAllHistoryQuery(userId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Добавить историю обучения.
    /// </summary>
    /// <param name="goal"> Модель данных цели. </param>
    [HttpPost]
    [ProducesResponseType(typeof(LearningHistoryResponseDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> AddHistory([FromBody] LearningHistoryRequestDto goal)
    {
        var command = _mapper.Map<AddHistoryCommand>(goal);
        command.UserId = _userAccessor.GetUserId();

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Обновить дату завершения обучения.
    /// </summary>
    /// <param name="id"> Идентификатор истории обучения. </param>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(Unit), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> UpdateHistoryDate(Guid id)
    {
        var userId = _userAccessor.GetUserId();
        var command = new UpdateHistoryCommand(id, userId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Удалить историю обучения.
    /// </summary>
    /// <param name="id"> Идентификатор истории обучения. </param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Unit), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> DeleteHistory(Guid id)
    {
        var userId = _userAccessor.GetUserId();
        var command = new DeleteHistoryCommand(id, userId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}
