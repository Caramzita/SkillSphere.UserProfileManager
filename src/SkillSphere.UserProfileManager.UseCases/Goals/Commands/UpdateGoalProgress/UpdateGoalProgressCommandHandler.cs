using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.UpdateGoalProgress;

public class UpdateGoalProgressCommandHandler : IRequestHandler<UpdateGoalProgressCommand, Result<Unit>>
{
    private readonly IGoalRepository _goalRepository;

    private readonly IUserProfileRepository _userProfileRepository;

    public UpdateGoalProgressCommandHandler(IGoalRepository goalRepository, 
        IUserProfileRepository userProfileRepository)
    {
        _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
    }

    public Task<Result<Unit>> Handle(UpdateGoalProgressCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
