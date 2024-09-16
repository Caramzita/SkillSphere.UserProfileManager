namespace SkillSphere.UserProfileManager.Contracts.DTOs;

public class LearningHistoryDto
{
    public string CourseTitle { get; set; } = string.Empty;

    public DateTime CompletedDate { get; set; }

    public string Description { get; set; } = string.Empty;
}
