namespace CVGenerator.Domain.Entities;

/// <summary>
/// Core CV entity representing a complete curriculum vitae
/// </summary>
public class CV
{
    public Guid Id { get; set; }
    public PersonalInfo PersonalInfo { get; set; } = new();
    public List<Education> Education { get; set; } = new();
    public List<WorkExperience> WorkExperience { get; set; } = new();
    public List<Skill> Skills { get; set; } = new();
    public List<string> Languages { get; set; } = new();
    public List<Certification> Certifications { get; set; } = new();
    public string? Summary { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class PersonalInfo
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

public class Education
{
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public double? GPA { get; set; }
}

public class WorkExperience
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

public class Skill
{
    public string Name { get; set; } = string.Empty;
    public SkillLevel Level { get; set; }
    public string? Category { get; set; }
}

public enum SkillLevel
{
    Beginner,
    Intermediate,
    Advanced,
    Expert
}

public class Certification
{
    public string Name { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? CredentialId { get; set; }
    public string? CredentialUrl { get; set; }
}
