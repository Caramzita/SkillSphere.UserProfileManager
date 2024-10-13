namespace SkillSphere.UserProfileManager.Core.Models;

public abstract class BaseModel
{
    public Guid Id { get; init; }

    public Guid UserId {  get; init; }
}
