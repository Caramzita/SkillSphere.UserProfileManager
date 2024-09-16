using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;
using SkillSphere.UserProfileManager.DataAccess.Entities;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.AddGoal;

public class AddGoalCommandHandler : IRequestHandler<AddGoalCommand, Result<Goal>>
{
    //private readonly IGoalRepository _goalRepository;
    private readonly IRepository<GoalEntity> _goalRepository;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly ILogger<AddGoalCommandHandler> _logger;

    private readonly IMapper _mapper;

    public AddGoalCommandHandler(IRepository<GoalEntity> goalRepository,
        IUserProfileRepository userProfileRepository,
        ILogger<AddGoalCommandHandler> logger,
        IMapper mapper)
    {
        _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<Goal>> Handle(AddGoalCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId).ConfigureAwait(false);

        if (userProfile == null)
        {
            return Result<Goal>.Invalid("Профиль пользователя не найден");
        }

        var goal = new Goal(request.UserId, request.Title);
        var goalEntity = _mapper.Map<GoalEntity>(goal);

        using (var transaction = await _goalRepository.BeginTransactionAsync().ConfigureAwait(false))
        {
            try
            {
                await _goalRepository.AddAsync(goalEntity).ConfigureAwait(false);
                userProfile.AddGoal(goal);
                await _userProfileRepository.UpdateProfile(userProfile).ConfigureAwait(false);
                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                _logger.LogError(ex, "Ошибка при добавлении цели");
                return Result<Goal>.Invalid("Ошибка при добавлении цели");
            }
        }

        return Result<Goal>.Success(goal);
    }
}