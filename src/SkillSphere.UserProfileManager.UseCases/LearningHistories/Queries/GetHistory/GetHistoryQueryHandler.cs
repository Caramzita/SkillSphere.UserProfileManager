using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.LearningHistory;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Queries.GetHistory;

public class GetHistoryQueryHandler : IRequestHandler<GetHistoryQuery, Result<LearningHistoryResponseDto>>
{
    private readonly IRepository<LearningHistory> _historyRepository;

    private readonly IMapper _mapper;

    public GetHistoryQueryHandler(IRepository<LearningHistory> historyRepository, IMapper mapper)
    {
        _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<LearningHistoryResponseDto>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
    {
        var history = await _historyRepository.GetByIdAsync(request.Id);

        if (history == null)
        {
            return Result<LearningHistoryResponseDto>.Invalid("История не найдена.");
        }

        var historyDto = _mapper.Map<LearningHistoryResponseDto>(history);

        return Result<LearningHistoryResponseDto>.Success(historyDto);
    }
}