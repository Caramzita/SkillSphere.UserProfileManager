using MediatR;
using Microsoft.Extensions.Logging;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.DeleteHistory;

public class DeleteHistoryCommandHandler : IRequestHandler<DeleteHistoryCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly IRepository<LearningHistory> _historyRepository;

    private readonly ILogger<DeleteHistoryCommandHandler> _logger;

    public DeleteHistoryCommandHandler(IUnitOfWork unitOfWork,
        IUserProfileRepository userProfileRepository,
        IRepository<LearningHistory> historyRepository,
        ILogger<DeleteHistoryCommandHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Unit>> Handle(DeleteHistoryCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<Unit>.Invalid("Профиль пользователя не найден");
        }

        var history = await _historyRepository.GetByIdAsync(request.Id);

        if (history == null)
        {
            return Result<Unit>.Invalid("Такой истории не существует");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            _historyRepository.Delete(history);
            userProfile.DeleteLearningHistory(history);

            _userProfileRepository.UpdateProfile(userProfile);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();

            return Result<Unit>.Empty();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            return Result<Unit>.Invalid("Ошибка при добавлении истории");
        };
    }
}
