using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.UserSkill;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.UseCases.UserSkills.Commands.AddUserSkill;

public class AddUserSkillCommandHandler : IRequestHandler<AddUserSkillCommand, Result<UserSkillResponseDto>>
{
    private readonly IUserSkillRepository _userSkillRepository;

    private readonly IUserProfileRepository _userProfileRepository;

    private readonly ISkillRepository _skillRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public AddUserSkillCommandHandler(IUserSkillRepository userSkillRepository, 
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

    public async Task<Result<UserSkillResponseDto>> Handle(AddUserSkillCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetProfileByUserId(request.UserId);

        if (userProfile == null)
        {
            return Result<UserSkillResponseDto>.Invalid("Такой профиль не существует");
        }

        if ( userProfile.Skills.Count >= 15)
        {
            return Result<UserSkillResponseDto>.Invalid("Пользователь не может иметь больше 15 навыков");
        }

        var skill = await _skillRepository.GetSkillById(request.SkillId);

        if (skill == null)
        {
            return Result<UserSkillResponseDto>.Invalid("Такой навык не найден");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var userSkill = new UserSkill(request.UserId, request.SkillId);

            userProfile.AddSkill(userSkill);         
            await _userSkillRepository.AddUserSkill(userSkill);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();

            var userSkillDto = _mapper.Map<UserSkillResponseDto>(userSkill);
            userSkillDto.SkillName = skill.Name;
            userSkillDto.CategoryName = skill.Category.Name;

            return Result<UserSkillResponseDto>.Success(userSkillDto);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            return Result<UserSkillResponseDto>.Invalid("An error occurred while adding the skill to user.");
        }
    }
}
