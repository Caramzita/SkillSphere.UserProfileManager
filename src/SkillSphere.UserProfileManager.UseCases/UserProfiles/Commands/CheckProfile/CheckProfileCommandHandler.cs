using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.CheckProfile;

public class CheckProfileCommandHandler : IRequestHandler<CheckProfileCommand, Result<bool>>
{
    private readonly IUserProfileRepository _userProfileRepository;

    public CheckProfileCommandHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
    }

    public async Task<Result<bool>> Handle(CheckProfileCommand request, CancellationToken cancellationToken)
    {
        var profileExists = await _userProfileRepository.ProfileExists(request.UserId);
        return Result<bool>.Success(profileExists);
    }
}
