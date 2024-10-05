using MediatR;
using Microsoft.Extensions.Logging;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.UpdateGoalProgress;

public class UpdateGoalProgressCommandHandler : IRequestHandler<UpdateGoalProgressCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly IRepository<Goal> _goalRepository;

    private readonly ILogger<UpdateGoalProgressCommandHandler> _logger;

    public UpdateGoalProgressCommandHandler(IUnitOfWork unitOfWork, 
        IUserProfileRepository userProfileRepository, 
        IRepository<Goal> goalRepository,
        ILogger<UpdateGoalProgressCommandHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Unit>> Handle(UpdateGoalProgressCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<Unit>.Invalid("Профиль пользователя не найден");
        }

        var goal = await _goalRepository.GetByIdAsync(request.Id);

        if (goal == null)
        {
            return Result<Unit>.Invalid("Такой цели не существует");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            goal.Progress = request.Progress;
            _goalRepository.UpdateAsync(goal);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();

            return Result<Unit>.Empty();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            return Result<Unit>.Invalid("Ошибка при добавлении цели");
        };
    }
}
