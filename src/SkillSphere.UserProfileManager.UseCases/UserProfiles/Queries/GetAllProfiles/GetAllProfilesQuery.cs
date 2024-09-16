using MediatR;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.UserProfiles.Queries.GetAllProfiles;

public class GetAllProfilesQuery : IStreamRequest<UserProfile> { }
