using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Queries.GetAllGoals;

public class GetAllGoalsQuery : IRequest<Result<IEnumerable<Goal>>>;

public class GetAllGoalsQueryHandler : IRequestHandler<GetAllGoalsQuery, Result<IEnumerable<Goal>>>
{
    private readonly IRepository<Goal> _goalRepository;

    public GetAllGoalsQueryHandler(IRepository<Goal> goalRepository)
    {
        _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
    }

    public async Task<Result<IEnumerable<Goal>>> Handle(GetAllGoalsQuery request, CancellationToken cancellationToken)
    {
        var goals = await _goalRepository.GetAllAsync();

        if (goals == null || !goals.Any())
        {
            return Result<IEnumerable<Goal>>.Invalid("Цели не найдены.");
        }

        return Result<IEnumerable<Goal>>.Success(goals);
    }
}
