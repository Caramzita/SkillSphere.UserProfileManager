using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.DeleteProfile;

public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, Result<Unit>>
{
    private readonly IUserProfileRepository _profileRepository;

    public DeleteProfileCommandHandler(IUserProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        var existingProfile = await _profileRepository.GetProfileByUserId(request.UserId);

        if (existingProfile == null)
        {
            return Result<Unit>.Invalid("Профиль не существует");
        }

        await _profileRepository.DeleteProfile(existingProfile);

        return Result<Unit>.Empty();
    }
}
