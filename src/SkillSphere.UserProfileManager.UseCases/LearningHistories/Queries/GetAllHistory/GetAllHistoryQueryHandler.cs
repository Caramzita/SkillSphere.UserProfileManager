using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Queries.GetAllHistory;

public class GetAllHistoryQueryHandler : IRequestHandler<GetAllHistoryQuery, Result<IEnumerable<LearningHistory>>>
{
    private readonly IRepository<LearningHistory> _historyRepository;

    public GetAllHistoryQueryHandler(IRepository<LearningHistory> historyRepository)
    {
        _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
    }

    public async Task<Result<IEnumerable<LearningHistory>>> Handle(GetAllHistoryQuery request, CancellationToken cancellationToken)
    {
        var histories = await _historyRepository.GetAllAsync(request.UserId);

        if (histories == null || !histories.Any())
        {
            return Result<IEnumerable<LearningHistory>>.Invalid("Истории не найдены.");
        }

        return Result<IEnumerable<LearningHistory>>.Success(histories);
    }
}
