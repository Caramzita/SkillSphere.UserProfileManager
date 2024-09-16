using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillSphere.Infrastructure.Security.UserAccessor;

namespace SkillSphere.UserProfileManager.API.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetGoals()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddGoal()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGoalProgress()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGoal(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
