using AutoMapper;
using CVGenerator.Application.DTOs;
using CVGenerator.Domain.Entities;
using CVGenerator.Domain.Interfaces;

namespace CVGenerator.Application.Services;

/// <summary>
/// Application service for CV operations following Single Responsibility Principle
/// </summary>
public interface ICVService
{
    Task<GenerateCVResponse> GenerateCVAsync(GenerateCVRequest request);
}

/// <summary>
/// Implementation of CV service with dependency injection
/// </summary>
public class CVService : ICVService
{
    private readonly ICVGeneratorService _cvGeneratorService;
    private readonly IMapper _mapper;

    public CVService(ICVGeneratorService cvGeneratorService, IMapper mapper)
    {
        _cvGeneratorService = cvGeneratorService ?? throw new ArgumentNullException(nameof(cvGeneratorService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<GenerateCVResponse> GenerateCVAsync(GenerateCVRequest request)
    {
        // Map DTO to domain entity
        var cv = _mapper.Map<CV>(request.CVData);
        cv.Id = Guid.NewGuid();
        cv.CreatedAt = DateTime.UtcNow;

        // Parse template
        var template = ParseTemplate(request.Template);

        // Generate PDF
        var pdfContent = await _cvGeneratorService.GeneratePdfAsync(cv, template);

        // Create response
        var response = new GenerateCVResponse
        {
            CVId = cv.Id,
            PdfContent = pdfContent,
            FileName = $"CV_{cv.PersonalInfo.LastName}_{cv.PersonalInfo.FirstName}_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf",
            GeneratedAt = DateTime.UtcNow
        };

        return response;
    }

    private CVTemplate ParseTemplate(string template)
    {
        return template.ToLower() switch
        {
            "modern" => CVTemplate.Modern,
            "classic" => CVTemplate.Classic,
            "creative" => CVTemplate.Creative,
            _ => CVTemplate.Modern
        };
    }
}
