using MediatR;
using SkillSphere.Infrastructure.UseCases;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.UpdateHistoryDate;

public class UpdateHistoryCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; }

    public Guid UserId { get; }

    public UpdateHistoryCommand(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}
