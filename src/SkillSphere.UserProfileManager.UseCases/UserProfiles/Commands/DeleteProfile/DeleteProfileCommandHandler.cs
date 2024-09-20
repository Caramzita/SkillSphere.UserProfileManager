using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.DeleteProfile;

public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository;

    public DeleteProfileCommandHandler(IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
    }

    public async Task<Result<Unit>> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        var existingProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (existingProfile == null)
        {
            return Result<Unit>.Invalid("Профиль не существует");
        }

        _userProfileRepository.DeleteProfile(existingProfile);
        await _unitOfWork.CompleteAsync();

        return Result<Unit>.Empty();
    }
}
