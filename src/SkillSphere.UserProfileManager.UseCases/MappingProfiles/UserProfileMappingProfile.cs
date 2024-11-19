using AutoMapper;
using SkillSphere.UserProfileManager.Contracts.DTOs.Goal;
using SkillSphere.UserProfileManager.Contracts.DTOs.LearningHistory;
using SkillSphere.UserProfileManager.Contracts.DTOs.Skill;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserSkill;
using SkillSphere.UserProfileManager.Core.Models.Skill;
using SkillSphere.UserProfileManager.Core.Models;

namespace SkillSphere.UserProfileManager.UseCases.Profiles;

public class UserProfileMappingProfile : Profile
{
    public UserProfileMappingProfile()
    {
        CreateMap<UserProfile, UserProfileSummaryDto>();

        CreateMap<UserProfile, UserProfileDetailDto>()
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills))
            .ForMember(dest => dest.Goals, opt => opt.MapFrom(src => src.Goals))
            .ForMember(dest => dest.LearningHistories, opt => opt.MapFrom(src => src.LearningHistories));

        CreateMap<UserSkill, UserSkillResponseDto>()
            .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill.Name));

        CreateMap<Goal, GoalResponseDto>();

        CreateMap<LearningHistory, LearningHistoryResponseDto>();

        CreateMap<Skill, SkillResponseDto>()
            .ForMember(dest => dest.SkillId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Skill, SkillsListRequestDto>()
            .ForMember(dest => dest.SkillId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Name));
    }
}
