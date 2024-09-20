using AutoMapper;
using SkillSphere.UserProfileManager.Contracts.DTOs;
using SkillSphere.UserProfileManager.UseCases.Goals.Commands.AddGoal;
using SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.AddHistory;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteSkill;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.CreateProfile;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.UpdateProfile;

namespace SkillSphere.UserProfileManager.API;

public class ControllerMappingProfile : Profile
{
    public ControllerMappingProfile()
    {
        CreateMap<UserProfileDto, CreateProfileCommand>();

        CreateMap<UserProfileDto, UpdateProfileCommand>();

        CreateMap<SkillDto, AddSkillCommand>();

        CreateMap<SkillDto, DeleteSkillCommand>();

        CreateMap<GoalDto, AddGoalCommand>();

        CreateMap<LearningHistoryDto, AddHistoryCommand>();
    }
}
