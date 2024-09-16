using MediatR;
using SkillSphere.Infrastructure.UseCases;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.DeleteProfile;

public class DeleteProfileCommand : IRequest<Result<Unit>>
{
    public Guid UserId { get; }

    public DeleteProfileCommand(Guid userId)
    {
        UserId = userId;
    }
}
