using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Queries.GetHistory;

public class GetHistoryQueryHandler : IRequestHandler<GetHistoryQuery, Result<LearningHistory>>
{
    private readonly IRepository<LearningHistory> _historyRepository;

    public GetHistoryQueryHandler(IRepository<LearningHistory> historyRepository)
    {
        _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
    }

    public async Task<Result<LearningHistory>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
    {
        var history = await _historyRepository.GetByIdAsync(request.Id, request.UserId);

        if (history == null)
        {
            return Result<LearningHistory>.Invalid("История не найдена.");
        }

        return Result<LearningHistory>.Success(history);
    }
}