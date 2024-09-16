using System.Text.Json.Serialization;

namespace SkillSphere.UserProfileManager.Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GoalProgress
{
    NotStarted,
    InProgress,
    Completed
}
