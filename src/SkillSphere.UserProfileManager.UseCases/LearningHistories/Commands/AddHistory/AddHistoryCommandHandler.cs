using MediatR;
using Microsoft.Extensions.Logging;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.AddHistory;

public class AddHistoryCommandHandler : IRequestHandler<AddHistoryCommand, Result<LearningHistory>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IRepository<LearningHistory> _historyRepository;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly ILogger<AddHistoryCommandHandler> _logger;

    public AddHistoryCommandHandler(IUnitOfWork unitOfWork, 
        IRepository<LearningHistory> historyRepository, 
        IUserProfileRepository userProfileRepository,
        ILogger<AddHistoryCommandHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<LearningHistory>> Handle(AddHistoryCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<LearningHistory>.Invalid("Профиль пользователя не найден");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var history = new LearningHistory(request.UserId, 
                request.CourseTitle, 
                request.Description, 
                request.CompletedDate);

            await _historyRepository.AddAsync(history);
            userProfile.AddLearningHistory(history);

            _userProfileRepository.UpdateProfile(userProfile);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();

            return Result<LearningHistory>.Success(history);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            return Result<LearningHistory>.Invalid("Ошибка при добавлении истории");
        }
    }
}
