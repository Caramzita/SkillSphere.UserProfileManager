using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteSkill;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ISkillRepository _skillRepository;

    public DeleteSkillCommandHandler(IUnitOfWork unitOfWork, ISkillRepository skillRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
    }

    public async Task<Result<Unit>> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetSkillById(request.SkillId);

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
        catch (Exception)
        {
            return Result<Unit>.Invalid("An error occurred while deleting the skill.");
        };
    }
}
