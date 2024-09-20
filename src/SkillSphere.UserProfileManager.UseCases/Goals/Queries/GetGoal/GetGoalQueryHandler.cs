using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Queries.GetGoal;

public class GetGoalQueryHandler : IRequestHandler<GetGoalQuery, Result<Goal>>
{
    private readonly IRepository<Goal> _goalRepository;

    public GetGoalQueryHandler(IRepository<Goal> goalRepository)
    {
        _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
    }

    public async Task<Result<Goal>> Handle(GetGoalQuery request, CancellationToken cancellationToken)
    {
        var goal = await _goalRepository.GetByIdAsync(request.Id, request.UserId);

        if (goal == null)
        {
            return Result<Goal>.Invalid("Цель не найдена.");
        }

        return Result<Goal>.Success(goal);
    }
}
