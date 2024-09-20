using MediatR;
using Microsoft.Extensions.Logging;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.DeleteGoal;

public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly IRepository<Goal> _goalRepository;

    private readonly ILogger<DeleteGoalCommandHandler> _logger;

    public DeleteGoalCommandHandler(IUnitOfWork unitOfWork, 
        IUserProfileRepository userProfileRepository, 
        IRepository<Goal> goalRepository,
        ILogger<DeleteGoalCommandHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Unit>> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<Unit>.Invalid("Профиль пользователя не найден");
        }

        var goal = await _goalRepository.GetByIdAsync(request.Id, request.UserId);

        if (goal == null)
        {
            return Result<Unit>.Invalid("Такой цели не существует");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            _goalRepository.Delete(goal);
            userProfile.DeleteGoal(goal);

            _userProfileRepository.UpdateProfile(userProfile);

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
