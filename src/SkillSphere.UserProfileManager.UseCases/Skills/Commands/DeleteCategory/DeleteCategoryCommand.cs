using MediatR;
using SkillSphere.Infrastructure.UseCases;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; }

    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }
}
