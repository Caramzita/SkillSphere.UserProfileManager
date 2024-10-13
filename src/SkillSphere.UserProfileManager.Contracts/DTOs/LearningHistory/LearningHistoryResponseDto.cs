namespace SkillSphere.UserProfileManager.Contracts.DTOs.LearningHistory;

public class LearningHistoryResponseDto
{
    public Guid Id { get; set; }

    public string CourseTitle { get; private set; } = string.Empty;

    public DateTime CompletedDate { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; init; }
}
