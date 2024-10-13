using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.LearningHistory;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Queries.GetAllHistory;

public record GetAllHistoryQuery(Guid UserId) : IRequest<Result<IEnumerable<LearningHistoryResponseDto>>>;

public class GetAllHistoryQueryHandler : IRequestHandler<GetAllHistoryQuery, Result<IEnumerable<LearningHistoryResponseDto>>>
{
    private readonly IRepository<LearningHistory> _historyRepository;

    private readonly IMapper _mapper;

    public GetAllHistoryQueryHandler(IRepository<LearningHistory> historyRepository, IMapper mapper)
    {
        _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<IEnumerable<LearningHistoryResponseDto>>> Handle(GetAllHistoryQuery request, 
        CancellationToken cancellationToken)
    {
        var histories = await _historyRepository.GetAllAsync(request.UserId);

        if (histories == null || !histories.Any())
        {
            return Result<IEnumerable<LearningHistoryResponseDto>>.Invalid("Истории не найдены.");
        }

        var historiesDto = _mapper.Map<IEnumerable<LearningHistoryResponseDto>>(histories);

        return Result<IEnumerable<LearningHistoryResponseDto>>.Success(historiesDto);
    }
}
