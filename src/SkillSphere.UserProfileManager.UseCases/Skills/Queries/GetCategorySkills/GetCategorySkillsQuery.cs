using AutoMapper;
using MediatR;
using SkillSphere.UserProfileManager.Contracts.DTOs.Skill;
using SkillSphere.UserProfileManager.Core.Interfaces;
using System.Runtime.CompilerServices;

//namespace SkillSphere.UserProfileManager.UseCases.Skills.Queries.GetCategorySkills;

//public record GetCategorySkillsQuery(Guid CategoryId) : IStreamRequest<SkillResponseDto>;

//public class GetCategorySkillsQueryHandler : IStreamRequestHandler<GetCategorySkillsQuery, SkillResponseDto>
//{
//    private readonly ISkillRepository _skillRepository;

//    private readonly IMapper _mapper;

//    public GetCategorySkillsQueryHandler(ISkillRepository skillRepository, IMapper mapper)
//    {
//        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
//        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
//    }

//    public async IAsyncEnumerable<SkillResponseDto> Handle(GetCategorySkillsQuery request, 
//        [EnumeratorCancellation] CancellationToken cancellationToken)
//    {
//        await foreach (var skill in _skillRepository.GetCategorySkills(request.CategoryId))
//        {
//            yield return _mapper.Map<SkillResponseDto>(skill);
//        }
//    }
//}
