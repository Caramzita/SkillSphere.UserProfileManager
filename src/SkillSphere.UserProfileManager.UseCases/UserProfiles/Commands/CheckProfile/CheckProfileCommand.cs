using MediatR;
using SkillSphere.Infrastructure.UseCases;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.CheckProfile;

public class CheckProfileCommand : IRequest<Result<bool>>
{
    public Guid UserId { get; }

    public CheckProfileCommand(Guid userId)
    {
        UserId = userId;
    }
}
