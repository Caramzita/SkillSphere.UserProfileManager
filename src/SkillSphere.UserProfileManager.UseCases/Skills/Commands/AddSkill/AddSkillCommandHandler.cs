using MediatR;
using Microsoft.Extensions.Logging;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;

public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, Result<Skill>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly IRepository<Skill> _skillRepository;

    private readonly ILogger<AddSkillCommandHandler> _logger;

    public AddSkillCommandHandler(IUnitOfWork unitOfWork, 
        IUserProfileRepository userProfileRepository, 
        IRepository<Skill> skillRepository,
        ILogger<AddSkillCommandHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Skill>> Handle(AddSkillCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<Skill>.Invalid("Профиль пользователя не найден");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var skill = new Skill(request.UserId, request.Name, request.Level);

            await _skillRepository.AddAsync(skill);
            userProfile.AddSkill(skill);

            _userProfileRepository.UpdateProfile(userProfile);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();

            return Result<Skill>.Success(skill);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            return Result<Skill>.Invalid("Ошибка при добавлении цели");
        }
    }
}
