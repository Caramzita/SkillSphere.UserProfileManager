using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.UserProfileManager.Contracts.DTOs.Skill;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.Core.Models.Skill;

namespace SkillSphere.UserProfileManager.UseCases.Skills.Commands.AddSkill;

public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, Result<SkillResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ISkillRepository _skillRepository;

    private readonly IMapper _mapper;

    public AddSkillCommandHandler(IUnitOfWork unitOfWork, 
        ISkillRepository skillRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<SkillResponseDto>> Handle(AddSkillCommand request, CancellationToken cancellationToken)
    {      
        var category = await _skillRepository.GetCategoryById(request.CategoryId);

        if (category == null)
        {
            return Result<SkillResponseDto>.Invalid("Нет такой категории");
        }

        var skill = new Skill(request.Name, request.CategoryId);

        try
        {
            await _skillRepository.AddSkill(skill);
            await _unitOfWork.CompleteAsync();

            var skillDto = _mapper.Map<SkillResponseDto>(skill);
            skillDto.CategoryName = category.Name;

            return Result<SkillResponseDto>.Success(skillDto);
        }
        catch (Exception)
        {
            return Result<SkillResponseDto>.Invalid("An error occurred while adding the skill.");
        }
    }
}
