using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Queries.GetAllHistory;

public class GetAllHistoryQuery : IRequest<Result<IEnumerable<LearningHistory>>>
{
    public Guid UserId { get; }

    public GetAllHistoryQuery(Guid userId)
    {
        UserId = userId;
    }
}
