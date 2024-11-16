using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserSkill;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.UseCases.UserSkills.Commands.AddUserSkills;

public class AddUserSkillsCommandHandler : IRequestHandler<AddUserSkillsCommand, Result<List<UserSkillResponseDto>>>
{
    private readonly IUserSkillRepository _userSkillRepository;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly ISkillRepository _skillRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public AddUserSkillsCommandHandler(IUserSkillRepository userSkillRepository, 
        IUserProfileRepository userProfileRepository,
        ISkillRepository skillRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _userSkillRepository = userSkillRepository ?? throw new ArgumentNullException(nameof(userSkillRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<List<UserSkillResponseDto>>> Handle(AddUserSkillsCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<List<UserSkillResponseDto>>.Invalid("Такой профиль не существует");
        }

        if ( userProfile.Skills.Count >= 15)
        {
            return Result<List<UserSkillResponseDto>>.Invalid("Пользователь не может иметь больше 15 навыков");
        }

        var skills = await _skillRepository.GetSkillsByIdsAsync(request.SkillIds);

        if (skills == null || skills.Count != request.SkillIds.Count)
        {
            return Result<List<UserSkillResponseDto>>.Invalid("Некоторые навыки не были найдены.");
        }

        var addedSkills = new List<UserSkillResponseDto>();

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            foreach (var skill in skills)
            {
                if (userProfile.Skills.Any(us => us.SkillId == skill.Id))
                {
                    continue;
                }

                var userSkill = new UserSkill(request.UserId, skill.Id);

                userProfile.AddSkill(userSkill);
                await _userSkillRepository.AddUserSkill(userSkill);

                var userSkillDto = _mapper.Map<UserSkillResponseDto>(userSkill);
                userSkillDto.SkillName = skill.Name;
                //userSkillDto.CategoryName = skill.Category.Name;

                addedSkills.Add(userSkillDto);
            }

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();

            return Result<List<UserSkillResponseDto>>.Success(addedSkills);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            return Result<List<UserSkillResponseDto>>.Invalid("An error occurred while adding the skill to user.");
        }
    }
}
