using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.UserSkills.Commands.DeleteUserSkill;

public class DeleteUserSkillCommandHandler : IRequestHandler<DeleteUserSkillCommand, Result<Unit>>
{
    private readonly IUserSkillRepository _userSkillRepository;

    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserSkillCommandHandler(IUserSkillRepository userSkillRepository,
        IUnitOfWork unitOfWork)
    {
        _userSkillRepository = userSkillRepository ?? throw new ArgumentNullException(nameof(userSkillRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Unit>> Handle(DeleteUserSkillCommand request, CancellationToken cancellationToken)
    {
        var userSkill = await _userSkillRepository.GetUserSkillById(request.SkillId, request.UserId);

        if (userSkill == null)
        {
            return Result<Unit>.Invalid("Такого навыка у пользователя нет");
        }

        try
        {
            _userSkillRepository.RemoveUserSkill(userSkill);
            await _unitOfWork.CompleteAsync();

            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception)
        {
            return Result<Unit>.Invalid("An error occurred while deleting the user skill.");
        };
    }
}
