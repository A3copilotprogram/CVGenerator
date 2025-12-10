using CVGenerator.Domain.Entities;

namespace CVGenerator.Domain.Interfaces;

/// <summary>
/// Interface for CV generation service following Dependency Inversion Principle
/// </summary>
public interface ICVGeneratorService
{
    /// <summary>
    /// Generates a PDF document for the given CV using the specified template
    /// </summary>
    Task<byte[]> GeneratePdfAsync(CV cv, CVTemplate template);
}

/// <summary>
/// Available CV templates
/// </summary>
public enum CVTemplate
{
    Modern,
    Classic,
    Creative
}
