using AutoMapper;
using CVGenerator.Application.DTOs;
using CVGenerator.Domain.Entities;

namespace CVGenerator.Application.MappingProfiles;

/// <summary>
/// AutoMapper profile for mapping between DTOs and Domain entities
/// </summary>
public class CVMappingProfile : Profile
{
    public CVMappingProfile()
    {
        // CV mappings
        CreateMap<CreateCVRequest, CV>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<CV, CreateCVRequest>();

        // PersonalInfo mappings
        CreateMap<PersonalInfoDto, PersonalInfo>();
        CreateMap<PersonalInfo, PersonalInfoDto>();

        // Education mappings
        CreateMap<EducationDto, Education>();
        CreateMap<Education, EducationDto>();

        // WorkExperience mappings
        CreateMap<WorkExperienceDto, WorkExperience>();
        CreateMap<WorkExperience, WorkExperienceDto>();

        // Skill mappings
        CreateMap<SkillDto, Skill>()
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => ParseSkillLevel(src.Level)));
        
        CreateMap<Skill, SkillDto>()
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level.ToString()));

        // Certification mappings
        CreateMap<CertificationDto, Certification>();
        CreateMap<Certification, CertificationDto>();
    }

    private SkillLevel ParseSkillLevel(string level)
    {
        return level.ToLower() switch
        {
            "beginner" => SkillLevel.Beginner,
            "intermediate" => SkillLevel.Intermediate,
            "advanced" => SkillLevel.Advanced,
            "expert" => SkillLevel.Expert,
            _ => SkillLevel.Intermediate
        };
    }
}
