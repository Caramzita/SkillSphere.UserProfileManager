using MediatR;
using SkillSphere.Infrastructure.UseCases;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.DeleteHistory;

public class DeleteHistoryCommand : IRequest<Result<Unit>>
{
    public Guid UserId { get; }

    public Guid Id { get; }

    public DeleteHistoryCommand(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}
