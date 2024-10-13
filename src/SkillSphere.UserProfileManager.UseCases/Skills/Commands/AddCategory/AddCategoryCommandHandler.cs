using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddCategory;

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Result<SkillCategory>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ISkillRepository _skillRepository;

    public AddCategoryCommandHandler(IUnitOfWork unitOfWork,
        ISkillRepository skillRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
    }

    public async Task<Result<SkillCategory>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new SkillCategory(request.CategoryName);

        try
        {
            await _skillRepository.AddCategory(category);
            await _unitOfWork.CompleteAsync();

            return Result<SkillCategory>.Success(category);
        }
        catch (Exception)
        {
            return Result<SkillCategory>.Invalid("An error occurred while adding the category.");
        }
    }
}
