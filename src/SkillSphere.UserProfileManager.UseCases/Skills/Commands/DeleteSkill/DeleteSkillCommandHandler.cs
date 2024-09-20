using MediatR;
using Microsoft.Extensions.Logging;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteSkill;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly IRepository<Skill> _skillRepository;

    private readonly ILogger<AddSkillCommandHandler> _logger;

    public DeleteSkillCommandHandler(IUnitOfWork unitOfWork, 
        IUserProfileRepository userProfileRepository, 
        IRepository<Skill> skillRepository,
        ILogger<AddSkillCommandHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Unit>> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<Unit>.Invalid("Профиль пользователя не найден");
        }

        var skill = await _skillRepository.GetByIdAsync(request.Id, request.UserId);

        if (skill == null)
        {
            return Result<Unit>.Invalid("Такого навыка не существует");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            _skillRepository.Delete(skill);
            userProfile.DeleteSkill(skill);

            _userProfileRepository.UpdateProfile(userProfile);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();

            return Result<Unit>.Empty();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            return Result<Unit>.Invalid("Ошибка при добавлении навыка");
        };
    }
}
