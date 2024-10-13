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

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public AddUserSkillCommandHandler(IUserSkillRepository userSkillRepository, 
        IUserProfileRepository userProfileRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _userSkillRepository = userSkillRepository ?? throw new ArgumentNullException(nameof(userSkillRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
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

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var userSkill = new UserSkill(request.UserId, request.SkillId);

            userProfile.AddSkill(userSkill);         
            await _userSkillRepository.AddUserSkill(userSkill);
            _userProfileRepository.UpdateProfile(userProfile);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();

            var userSkillDto = _mapper.Map<UserSkillResponseDto>(userSkill);

            return Result<UserSkillResponseDto>.Success(userSkillDto);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            return Result<UserSkillResponseDto>.Invalid("An error occurred while adding the skill to user.");
        }
    }
}
