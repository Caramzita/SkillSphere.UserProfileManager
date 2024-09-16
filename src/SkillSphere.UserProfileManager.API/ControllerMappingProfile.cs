using AutoMapper;
using SkillSphere.UserProfileManager.Contracts.DTOs;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;
using SkillSphere.UserProfileManager.UseCases.Skills.Commands.DeleteSkill;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.CreateProfile;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.UpdateProfile;

namespace SkillSphere.UserProfileManager.API;

public class ControllerMappingProfile : Profile
{
    public ControllerMappingProfile()
    {
        CreateMap<UserProfileDto, CreateProfileCommand>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("bio", opt => opt.MapFrom(src => src.Bio))
            .ForCtorParam("profilePictureUrl", opt => opt.MapFrom(src => src.ProfilePictureUrl));

        CreateMap<UserProfileDto, UpdateProfileCommand>();

        CreateMap<SkillDto, AddSkillCommand>();

        CreateMap<SkillDto, DeleteSkillCommand>();
    }
}
