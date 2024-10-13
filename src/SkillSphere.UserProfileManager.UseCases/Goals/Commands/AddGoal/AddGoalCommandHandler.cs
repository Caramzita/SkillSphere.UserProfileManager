using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.Goal;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Goals.Commands.AddGoal;

public class AddGoalCommandHandler : IRequestHandler<AddGoalCommand, Result<GoalResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository; 

    private readonly IRepository<Goal> _goalRepository;

    private readonly IMapper _mapper;

    public AddGoalCommandHandler(IUnitOfWork unitOfWork,
        IRepository<Goal> goalRepository,
        IMapper mapper,
        IUserProfileRepository userProfileRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
        _userProfileRepository = userProfileRepository;
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<GoalResponseDto>> Handle(AddGoalCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<GoalResponseDto>.Invalid("Профиль пользователя не найден");
        }

        if (userProfile.Goals.Count >= 10)
        {
            return Result<GoalResponseDto>.Invalid("Пользователь не может иметь больше 10 целей");
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

            var goalDto = _mapper.Map<GoalResponseDto>(goal);

            return Result<GoalResponseDto>.Success(goalDto);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            return Result<GoalResponseDto>.Invalid("Ошибка при добавлении цели");
        }
    }
}