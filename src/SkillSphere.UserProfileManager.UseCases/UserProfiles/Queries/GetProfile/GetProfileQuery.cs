using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Queries.GetProfile;

public class GetProfileQuery : IRequest<Result<UserProfile>>
{
    public Guid UserId { get; }

    public GetProfileQuery(Guid userId)
    {
        UserId = userId;
    }
}
