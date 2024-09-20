using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.AddHistory;

public class AddHistoryCommand : IRequest<Result<LearningHistory>>
{
    public Guid UserId { get; set; }

    public string CourseTitle { get; } = string.Empty;

    public DateTime CompletedDate { get; }

    public string Description { get; } = string.Empty;

    public AddHistoryCommand(string courseTitle, string description, DateTime completedDate)
    {
        CourseTitle = courseTitle;
        Description = description;
        CompletedDate = completedDate;
    }
}
