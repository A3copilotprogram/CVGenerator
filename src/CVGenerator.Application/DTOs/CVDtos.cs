namespace CVGenerator.Application.DTOs;

/// <summary>
/// DTO for creating a CV request
/// </summary>
public class CreateCVRequest
{
    public PersonalInfoDto PersonalInfo { get; set; } = new();
    public List<EducationDto> Education { get; set; } = new();
    public List<WorkExperienceDto> WorkExperience { get; set; } = new();
    public List<SkillDto> Skills { get; set; } = new();
    public List<string> Languages { get; set; } = new();
    public List<CertificationDto> Certifications { get; set; } = new();
    public string? Summary { get; set; }
}

public class PersonalInfoDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? LinkedIn { get; set; }
    public string? GitHub { get; set; }
    public string? Website { get; set; }
}

public class EducationDto
{
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public double? GPA { get; set; }
}

public class WorkExperienceDto
{
    public string JobTitle { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrentPosition { get; set; }
    public List<string> Responsibilities { get; set; } = new();
    public List<string> Achievements { get; set; } = new();
}

public class SkillDto
{
    public string Name { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty; // "Beginner", "Intermediate", "Advanced", "Expert"
    public string? Category { get; set; }
}

public class CertificationDto
{
    public string Name { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? CredentialId { get; set; }
    public string? CredentialUrl { get; set; }
}

/// <summary>
/// DTO for CV generation request
/// </summary>
public class GenerateCVRequest
{
    public CreateCVRequest CVData { get; set; } = new();
    public string Template { get; set; } = "Modern"; // "Modern", "Classic", "Creative"
}

/// <summary>
/// Response for generated CV
/// </summary>
public class GenerateCVResponse
{
    public Guid CVId { get; set; }
    public byte[] PdfContent { get; set; } = Array.Empty<byte>();
    public string FileName { get; set; } = string.Empty;
    public DateTime GeneratedAt { get; set; }
}
