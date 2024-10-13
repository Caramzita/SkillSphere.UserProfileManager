using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.LearningHistory;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.AddHistory;

public class AddHistoryCommandHandler : IRequestHandler<AddHistoryCommand, Result<LearningHistoryResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IRepository<LearningHistory> _historyRepository;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly IMapper _mapper;

    public AddHistoryCommandHandler(IUnitOfWork unitOfWork, 
        IRepository<LearningHistory> historyRepository, 
        IUserProfileRepository userProfileRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _historyRepository = historyRepository ?? throw new ArgumentNullException(nameof(historyRepository));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<LearningHistoryResponseDto>> Handle(AddHistoryCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<LearningHistoryResponseDto>.Invalid("Профиль пользователя не найден");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var history = new LearningHistory(request.UserId, 
                request.CourseTitle, 
                request.Description, 
                request.CompletedDate);

            userProfile.AddLearningHistory(history);
            await _historyRepository.AddAsync(history);

            _userProfileRepository.UpdateProfile(userProfile);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();

            var historyDto = _mapper.Map<LearningHistoryResponseDto>(history);

            return Result<LearningHistoryResponseDto>.Success(historyDto);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            return Result<LearningHistoryResponseDto>.Invalid("Ошибка при добавлении истории");
        }
    }
}
