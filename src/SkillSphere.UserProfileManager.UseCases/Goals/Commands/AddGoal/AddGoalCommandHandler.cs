using MediatR;
using Microsoft.Extensions.Logging;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.AddGoal;

public class AddGoalCommandHandler : IRequestHandler<AddGoalCommand, Result<Goal>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository; 

    private readonly IRepository<Goal> _goalRepository;

    private readonly ILogger<AddGoalCommandHandler> _logger;

    public AddGoalCommandHandler(IUnitOfWork unitOfWork,
        IRepository<Goal> goalRepository,
        ILogger<AddGoalCommandHandler> logger,
        IUserProfileRepository userProfileRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userProfileRepository = userProfileRepository;
    }

    public async Task<Result<Goal>> Handle(AddGoalCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<Goal>.Invalid("Профиль пользователя не найден");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var goal = new Goal(request.UserId, request.Title);

            await _goalRepository.AddAsync(goal);
            userProfile.AddGoal(goal);

            _userProfileRepository.UpdateProfile(userProfile);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();

            return Result<Goal>.Success(goal);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            return Result<Goal>.Invalid("Ошибка при добавлении цели");
        }
    }
}