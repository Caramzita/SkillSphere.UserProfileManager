using AutoMapper;
using SkillSphere.UserProfileManager.Core.Models;
using SkillSphere.UserProfileManager.DataAccess.Entities;

namespace SkillSphere.UserProfileManager.DataAccess;

public class RepositoryMappingProfile : Profile
{
    public RepositoryMappingProfile()
    {
        CreateMap<Goal, GoalEntity>().ReverseMap();

        CreateMap<UserProfile, UserProfileEntity>().ReverseMap();

        CreateMap<Skill, SkillEntity>().ReverseMap();

        CreateMap<LearningHistory, LearningHistoryEntity>().ReverseMap();
    }
}
