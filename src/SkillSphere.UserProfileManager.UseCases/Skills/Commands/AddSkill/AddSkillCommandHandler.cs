using MediatR;
using Microsoft.Extensions.Logging;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;

public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, Result<Skill>>
{
    private readonly ISkillRepository _skillRepository;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly ILogger<AddSkillCommandHandler> _logger;

    public AddSkillCommandHandler(ISkillRepository skillRepository,
        IUserProfileRepository userProfileRepository,
        ILogger<AddSkillCommandHandler> logger)
    {
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Skill>> Handle(AddSkillCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<Skill>.Invalid("Профиль пользователя не найден");
        }

        var skill = new Skill(request.UserId, request.Name, request.Level);

        using (var transaction = await _skillRepository.BeginTransactionAsync())
        {
            try
            {
                await _skillRepository.AddSkill(skill);
                userProfile.AddSkill(skill);
                await _userProfileRepository.UpdateProfile(userProfile);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Ошибка при добавлении навыка");
                return Result<Skill>.Invalid("Ошибка при добавлении навыка");
            }
        }

        return Result<Skill>.Success(skill);
    }
}
