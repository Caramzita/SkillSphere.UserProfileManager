namespace SkillSphere.UserProfileManager.Core.Models;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
}
