using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddCategory;

public class AddCategoryCommand : IRequest<Result<SkillCategory>>
{
    public string CategoryName { get; }

    public AddCategoryCommand(string categoryName)
    {
        CategoryName = categoryName;
    }
}
