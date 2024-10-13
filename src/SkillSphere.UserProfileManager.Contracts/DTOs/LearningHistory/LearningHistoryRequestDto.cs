namespace SkillSphere.UserProfileManager.Contracts.DTOs.LearningHistory;

public class LearningHistoryRequestDto
{
    public string CourseTitle { get; set; } = string.Empty;

    public DateTime CompletedDate { get; set; }

    public string Description { get; set; } = string.Empty;
}
