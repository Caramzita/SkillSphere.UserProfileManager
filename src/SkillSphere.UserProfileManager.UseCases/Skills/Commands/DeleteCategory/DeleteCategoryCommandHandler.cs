using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ISkillRepository _skillRepository;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ISkillRepository skillRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
    }

    public async Task<Result<Unit>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _skillRepository.GetCategoryById(request.Id);

        if (category == null)
        {
            return Result<Unit>.Invalid("Такой категории не существует");
        }

        try
        {
            _skillRepository.DeleteCategory(category);
            await _unitOfWork.CompleteAsync();

            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception)
        {
            return Result<Unit>.Invalid("An error occurred while deleting the category.");
        };
    }
}
