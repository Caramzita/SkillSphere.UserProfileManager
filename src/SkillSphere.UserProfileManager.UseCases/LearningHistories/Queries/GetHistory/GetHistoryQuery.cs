using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.LearningHistory;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Queries.GetHistory;

public class GetHistoryQuery : IRequest<Result<LearningHistoryResponseDto>>
{
    public Guid Id { get; }

    public Guid UserId { get; }

    public GetHistoryQuery(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}
