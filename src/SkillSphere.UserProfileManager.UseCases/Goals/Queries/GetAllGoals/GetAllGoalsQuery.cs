using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.Goal;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Queries.GetAllGoals;

public record GetAllGoalsQuery(Guid UserId) : IRequest<Result<IEnumerable<GoalResponseDto>>>;

public class GetAllGoalsQueryHandler : IRequestHandler<GetAllGoalsQuery, Result<IEnumerable<GoalResponseDto>>>
{
    private readonly IRepository<Goal> _goalRepository;

    private readonly IMapper _mapper;

    public GetAllGoalsQueryHandler(IRepository<Goal> goalRepository, IMapper mapper)
    {
        _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<IEnumerable<GoalResponseDto>>> Handle(GetAllGoalsQuery request, 
        CancellationToken cancellationToken)
    {
        var goals = await _goalRepository.GetAllAsync(request.UserId);

        if (goals == null || !goals.Any())
        {
            return Result<IEnumerable<GoalResponseDto>>.Invalid("Цели не найдены.");
        }

        var goalsDto = _mapper.Map<IEnumerable<GoalResponseDto>>(goals);

        return Result<IEnumerable<GoalResponseDto>>.Success(goalsDto);
    }
}
