using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Queries.GetHistory;

public class GetHistoryQuery : IRequest<Result<LearningHistory>>
{
    public Guid Id { get; }

    public Guid UserId { get; }

    public GetHistoryQuery(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}
