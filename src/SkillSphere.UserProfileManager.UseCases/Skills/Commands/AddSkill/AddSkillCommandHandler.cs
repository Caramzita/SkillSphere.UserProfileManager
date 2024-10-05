using MediatR;
using Microsoft.Extensions.Logging;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;

public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, Result<Skill>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ISkillRepository _skillRepository;

    private readonly ILogger<AddSkillCommandHandler> _logger;

    public AddSkillCommandHandler(IUnitOfWork unitOfWork, 
        ISkillRepository skillRepository,
        ILogger<AddSkillCommandHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Skill>> Handle(AddSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = new Skill(request.Name, request.CategoryId);

        try
        {
            await _skillRepository.AddSkill(skill);
            await _unitOfWork.CompleteAsync();

            return Result<Skill>.Success(skill);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding skill.");
            return Result<Skill>.Invalid("An error occurred while adding the skill.");
        }
    }
}
