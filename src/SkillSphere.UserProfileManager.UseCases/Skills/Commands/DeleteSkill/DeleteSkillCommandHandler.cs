using MediatR;
using Microsoft.Extensions.Logging;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteSkill;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, Result<Unit>>
{
    private readonly ISkillRepository _skillRepository;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly ILogger<AddSkillCommandHandler> _logger;

    public DeleteSkillCommandHandler(ISkillRepository skillRepository,
        IUserProfileRepository userProfileRepository,
        ILogger<AddSkillCommandHandler> logger)
    {
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Unit>> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<Unit>.Invalid("Профиль пользователя не найден");
        }

        var skill = await _skillRepository.GetSkillById(request.Id, request.UserId);

        if (skill == null)
        {
            return Result<Unit>.Invalid("Такой заметки не существует");
        }

        using (var transaction = await _skillRepository.BeginTransactionAsync())
        {
            try
            {
                await _skillRepository.DeleteSkill(skill);
                userProfile.DeleteSkill(skill);
                await _userProfileRepository.UpdateProfile(userProfile);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Ошибка при обновлении навыка");
                return Result<Unit>.Invalid("Ошибка при обновлении навыка");
            }
        }

        return Result<Unit>.Empty();
    }
}
