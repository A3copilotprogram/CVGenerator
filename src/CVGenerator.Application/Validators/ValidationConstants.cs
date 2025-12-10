namespace CVGenerator.Application.Validators;

/// <summary>
/// Constants used in validation rules
/// </summary>
public static class ValidationConstants
{
    // GPA validation (US grading system)
    public const double MinGPA = 0.0;
    public const double MaxGPA = 4.0;
    
    // String length limits
    public const int MaxNameLength = 50;
    public const int MaxDegreeLength = 100;
    public const int MaxInstitutionLength = 200;
    public const int MaxJobTitleLength = 100;
    public const int MaxCompanyLength = 200;
    public const int MaxSkillNameLength = 100;
    public const int MaxCertificationNameLength = 200;
    public const int MaxIssuerLength = 200;
    public const int MaxSummaryLength = 1000;
    public const int MaxPhoneLength = 20;
}
