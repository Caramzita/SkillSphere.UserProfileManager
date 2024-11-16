namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands;

public interface IProfileCommand
{
    public Guid UserId { get; }

    string Name { get; }

    string? ProfilePictureUrl { get; }

    string Bio { get; }
}