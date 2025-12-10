using CVGenerator.Application.DTOs;
using FluentValidation;

namespace CVGenerator.Application.Validators;

/// <summary>
/// Validator for CreateCVRequest following validation best practices
/// </summary>
public class CreateCVRequestValidator : AbstractValidator<CreateCVRequest>
{
    public CreateCVRequestValidator()
    {
        RuleFor(x => x.PersonalInfo)
            .NotNull()
            .SetValidator(new PersonalInfoDtoValidator());

        RuleFor(x => x.Education)
            .NotNull()
            .Must(x => x.Count > 0)
            .WithMessage("At least one education entry is required");

        RuleForEach(x => x.Education)
            .SetValidator(new EducationDtoValidator());

        RuleForEach(x => x.WorkExperience)
            .SetValidator(new WorkExperienceDtoValidator());

        RuleForEach(x => x.Skills)
            .SetValidator(new SkillDtoValidator());

        RuleForEach(x => x.Certifications)
            .SetValidator(new CertificationDtoValidator());

        RuleFor(x => x.Summary)
            .MaximumLength(1000)
            .WithMessage("Summary must not exceed 1000 characters");
    }
}

public class PersonalInfoDtoValidator : AbstractValidator<PersonalInfoDto>
{
    public PersonalInfoDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required")
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20)
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

        RuleFor(x => x.LinkedIn)
            .Must(BeValidUrl)
            .WithMessage("Invalid LinkedIn URL")
            .When(x => !string.IsNullOrEmpty(x.LinkedIn));

        RuleFor(x => x.GitHub)
            .Must(BeValidUrl)
            .WithMessage("Invalid GitHub URL")
            .When(x => !string.IsNullOrEmpty(x.GitHub));

        RuleFor(x => x.Website)
            .Must(BeValidUrl)
            .WithMessage("Invalid website URL")
            .When(x => !string.IsNullOrEmpty(x.Website));
    }

    private bool BeValidUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}

public class EducationDtoValidator : AbstractValidator<EducationDto>
{
    public EducationDtoValidator()
    {
        RuleFor(x => x.Degree)
            .NotEmpty()
            .WithMessage("Degree is required")
            .MaximumLength(100);

        RuleFor(x => x.Institution)
            .NotEmpty()
            .WithMessage("Institution is required")
            .MaximumLength(200);

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Start date cannot be in the future");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be after start date")
            .When(x => x.EndDate.HasValue);

        RuleFor(x => x.GPA)
            .InclusiveBetween(0, 4.0)
            .WithMessage("GPA must be between 0 and 4.0")
            .When(x => x.GPA.HasValue);
    }
}

public class WorkExperienceDtoValidator : AbstractValidator<WorkExperienceDto>
{
    public WorkExperienceDtoValidator()
    {
        RuleFor(x => x.JobTitle)
            .NotEmpty()
            .WithMessage("Job title is required")
            .MaximumLength(100);

        RuleFor(x => x.Company)
            .NotEmpty()
            .WithMessage("Company is required")
            .MaximumLength(200);

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Start date cannot be in the future");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be after start date")
            .When(x => x.EndDate.HasValue && !x.IsCurrentPosition);

        RuleFor(x => x.EndDate)
            .Null()
            .When(x => x.IsCurrentPosition)
            .WithMessage("End date should not be specified for current position");
    }
}

public class SkillDtoValidator : AbstractValidator<SkillDto>
{
    public SkillDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Skill name is required")
            .MaximumLength(100);

        RuleFor(x => x.Level)
            .NotEmpty()
            .WithMessage("Skill level is required")
            .Must(BeValidSkillLevel)
            .WithMessage("Skill level must be: Beginner, Intermediate, Advanced, or Expert");
    }

    private bool BeValidSkillLevel(string level)
    {
        var validLevels = new[] { "Beginner", "Intermediate", "Advanced", "Expert" };
        return validLevels.Contains(level, StringComparer.OrdinalIgnoreCase);
    }
}

public class CertificationDtoValidator : AbstractValidator<CertificationDto>
{
    public CertificationDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Certification name is required")
            .MaximumLength(200);

        RuleFor(x => x.Issuer)
            .NotEmpty()
            .WithMessage("Issuer is required")
            .MaximumLength(200);

        RuleFor(x => x.IssueDate)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Issue date cannot be in the future");

        RuleFor(x => x.ExpiryDate)
            .GreaterThan(x => x.IssueDate)
            .WithMessage("Expiry date must be after issue date")
            .When(x => x.ExpiryDate.HasValue);
    }
}

public class GenerateCVRequestValidator : AbstractValidator<GenerateCVRequest>
{
    public GenerateCVRequestValidator()
    {
        RuleFor(x => x.CVData)
            .NotNull()
            .SetValidator(new CreateCVRequestValidator());

        RuleFor(x => x.Template)
            .NotEmpty()
            .WithMessage("Template is required")
            .Must(BeValidTemplate)
            .WithMessage("Template must be: Modern, Classic, or Creative");
    }

    private bool BeValidTemplate(string template)
    {
        var validTemplates = new[] { "Modern", "Classic", "Creative" };
        return validTemplates.Contains(template, StringComparer.OrdinalIgnoreCase);
    }
}
