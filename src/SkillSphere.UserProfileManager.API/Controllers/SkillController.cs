using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.Skill;
using SkillSphere.UserProfileManager.Core.Models.Skill;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddCategory;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteCategory;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteSkill;
using SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetCategorySkills;
using SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkillById;
using SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkillCategories;

namespace SkillSphere.UserProfileManager.API.Controllers;

/// <summary>
/// Предоставляет Rest API для работы с навыками.
/// </summary>
[Route("api/categories")]
[ApiController]
[Authorize]
public class SkillController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="SkillController"/>.
    /// </summary>
    /// <param name="mediator"> Интерфейс для отправки команд и запросов через Mediator. </param>
    /// <param name="mapper"> Интерфейс для маппинга данных между моделями. </param>
    /// <exception cref="ArgumentNullException"> Ошибка загрузки интерфейса. </exception>
    public SkillController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Получить все категории навыков.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(SkillCategory), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public IAsyncEnumerable<SkillCategory> GetSkillCategories()
    {
        var query = new GetSkillCategoriesQuery();

        return _mediator.CreateStream(query);
    }

    /// <summary>
    /// Получить все навыки категории.
    /// </summary>
    /// <param name="categoryId"> Идентификатор категории. </param>
    [HttpGet("{categoryId:guid}/skills")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(SkillResponseDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public IAsyncEnumerable<SkillResponseDto> GetCategorySkills(Guid categoryId)
    {
        var query = new GetCategorySkillsQuery(categoryId);

        return _mediator.CreateStream(query);
    }

    /// <summary>
    /// Получить навык по идентификатору.
    /// </summary>
    /// <param name="categoryId"> Идентификатор категории. </param>
    /// <param name="skillId"> Идентификатор навыка. </param>
    [HttpGet("{categoryId:guid}/skills/{skillId:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(SkillResponseDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> GetSkillById(Guid categoryId, Guid skillId)
    {
        var command = new GetSkillByIdQuery(categoryId, skillId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Добавить навык.
    /// </summary>
    /// <param name="categoryId"> Идентификатор категории. </param>
    /// <param name="skillDto"> Модель данных навыка. </param>
    [HttpPost("{categoryId:guid}/skills")]
    [ProducesResponseType(typeof(SkillResponseDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> AddSkill(Guid categoryId, [FromBody] SkillRequestDto skillDto)
    {
        var command = new AddSkillCommand(skillDto.Name, categoryId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Удалить навык.
    /// </summary>
    /// <param name="categoryId"> Идентификатор категории. </param>
    /// <param name="skillId"> Идентификатор навыка. </param>
    [HttpDelete("{categoryId:guid}/skills/{skillId:guid}")]
    [ProducesResponseType(typeof(Unit), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> DeleteSkill(Guid categoryId, Guid skillId)
    {
        var command = new DeleteSkillCommand(categoryId, skillId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Добавить категорию.
    /// </summary>
    /// <param name="category"> Модель данных категории. </param>
    [HttpPost]
    [ProducesResponseType(typeof(SkillCategory), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> AddCategory([FromBody] CategoryRequestDto category)
    {
        var command = new AddCategoryCommand(category.Name);
        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Удалить категорию.
    /// </summary>
    /// <param name="categoryId"> Идентификатор категории. </param>
    [HttpDelete("{categoryId:guid}")]
    [ProducesResponseType(typeof(Unit), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        var command = new DeleteCategoryCommand(categoryId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}
