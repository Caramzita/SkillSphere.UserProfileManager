using AutoMapper;
using SkillSphere.UserProfileManager.Contracts.DTOs.Goal;
using SkillSphere.UserProfileManager.Contracts.DTOs.LearningHistory;
using SkillSphere.UserProfileManager.Contracts.DTOs.Skill;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserProfile;
using SkillSphere.UserProfileManager.UseCases.Goals.Commands.AddGoal;
using SkillSphere.UserProfileManager.UseCases.LearningHistories.Commands.AddHistory;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteSkill;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.CreateProfile;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.UpdateProfile;

namespace SkillSphere.UserProfileManager.API;

/// <summary>
/// Профиль AutoMapper для маппинга объектов запросов из контроллера на команды.
/// </summary>
public class ControllerMappingProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ControllerMappingProfile"/> и задает конфигурации маппинга.
    /// </summary>
    public ControllerMappingProfile()
    {
        CreateMap<UserProfileRequestDto, CreateProfileCommand>();

        CreateMap<UserProfileRequestDto, UpdateProfileCommand>();

        CreateMap<SkillRequestDto, AddSkillCommand>();

        CreateMap<SkillRequestDto, DeleteSkillCommand>();

        CreateMap<GoalRequestDto, AddGoalCommand>();

        CreateMap<LearningHistoryRequestDto, AddHistoryCommand>();   
    }
}