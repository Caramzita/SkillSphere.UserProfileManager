using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSphere.Infrastructure.Security.UserAccessor;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteSkill;
using SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetAllSkills;
using SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetSkill;

namespace SkillSphere.UserProfileManager.API.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            var userId = _userAccessor.GetUserId();
            var command = new GetAllSkillsQuery(userId);

            var result = await _mediator.Send(command);

            return result.ToActionResult();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSkill(Guid id)
        {
            var userId = _userAccessor.GetUserId();
            var command = new GetSkillQuery(id, userId);

            var result = await _mediator.Send(command);

            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill([FromBody] SkillDto skill)
        {
            var command = _mapper.Map<AddSkillCommand>(skill);
            command.UserId = _userAccessor.GetUserId();

            var result = await _mediator.Send(command);

            return result.ToActionResult();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            var userId = _userAccessor.GetUserId();
            var command = new DeleteSkillCommand(id, userId);

            var result = await _mediator.Send(command);

            return result.ToActionResult();
        }
    }
}
