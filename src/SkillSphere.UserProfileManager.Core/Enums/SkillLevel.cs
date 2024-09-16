using System.Text.Json.Serialization;

namespace SkillSphere.UserProfileManager.Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SkillLevel
{
    Beginner,
    Intermediate,
    Expert
}
