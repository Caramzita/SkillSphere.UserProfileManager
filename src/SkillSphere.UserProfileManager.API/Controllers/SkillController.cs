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
using SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkillCategories;
using SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkillsByIds;

namespace SkillSphere.UserProfileManager.API.Controllers;

/// <summary>
/// Предоставляет Rest API для работы с навыками.
/// </summary>
[Route("api")]
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
    [HttpGet("categories")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(SkillCategory), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public IAsyncEnumerable<SkillCategory> GetSkillCategories()
    {
        var query = new GetSkillCategoriesQuery();

        return _mediator.CreateStream(query);
    }

    ///// <summary>
    ///// Получить навык по идентификатору.
    ///// </summary>
    ///// <param name="skillId"> Идентификатор навыка. </param>
    //[HttpPost("skills/{skillId:guid}")]
    //[AllowAnonymous]
    //[ProducesResponseType(typeof(SkillResponseDto), 200)]
    //[ProducesResponseType(typeof(List<string>), 400)]
    //public async Task<IActionResult> GetSkillsByIds(List<Guid> skillId)
    //{
    //    var command = new GetSkillByIdQuery(skillId);

    //    var result = await _mediator.Send(command);

    //    return result.ToActionResult();
    //}

    /// <summary>
    /// Проверяет наличие навыков по переданным идентификаторам.
    /// </summary>
    /// <param name="skillIds"> Список идентификаторов навыков, которые необходимо проверить. </param>
    /// <returns></returns>
    [HttpPost("skills/check-skills")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<SkillResponseDto>), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> CheckSkills([FromBody] List<Guid> skillIds)
    {
        var query = new GetSkillsByIdsQuery(skillIds);

        var result = await _mediator.Send(query);

        return result.ToActionResult();
    }

    /// <summary>
    /// Добавить навык.
    /// </summary>
    /// <param name="categoryId"> Идентификатор категории. </param>
    /// <param name="skillDto"> Модель данных навыка. </param>
    [HttpPost("categories/{categoryId:guid}/skills")]
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
    [HttpDelete("categories/{categoryId:guid}/skills/{skillId:guid}")]
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
    [HttpPost("categories")]
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
    [HttpDelete("categories/{categoryId:guid}")]
    [ProducesResponseType(typeof(Unit), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        var command = new DeleteCategoryCommand(categoryId);

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}
