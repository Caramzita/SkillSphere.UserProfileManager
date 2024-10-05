using MediatR;
using Microsoft.Extensions.Logging;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteSkill;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ISkillRepository _skillRepository;

    private readonly ILogger<AddSkillCommandHandler> _logger;

    public DeleteSkillCommandHandler(IUnitOfWork unitOfWork, 
        ISkillRepository skillRepository,
        ILogger<AddSkillCommandHandler> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Unit>> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetSkillById(request.Id);

        if (skill == null)
        {
            return Result<Unit>.Invalid("Такого навыка не существует");
        }

        try
        {
            _skillRepository.DeleteSkill(skill);
            await _unitOfWork.CompleteAsync();

            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding skill.");
            return Result<Unit>.Invalid("An error occurred while deleting the skill.");
        };
    }
}
