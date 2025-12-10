using CVGenerator.Application.DTOs;
using CVGenerator.Application.Services;
using CVGenerator.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CVGenerator.API.Controllers;

/// <summary>
/// API Controller for CV generation operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CVController : ControllerBase
{
    private readonly ICVService _cvService;
    private readonly IValidator<GenerateCVRequest> _validator;
    private readonly ILogger<CVController> _logger;

    public CVController(
        ICVService cvService,
        IValidator<GenerateCVRequest> validator,
        ILogger<CVController> logger)
    {
        _cvService = cvService ?? throw new ArgumentNullException(nameof(cvService));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Generates a CV PDF from provided data
    /// </summary>
    /// <param name="request">CV data and template selection</param>
    /// <returns>PDF file download</returns>
    /// <response code="200">Returns the generated PDF file</response>
    /// <response code="400">If the request data is invalid</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpPost("generate")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GenerateCV([FromBody] GenerateCVRequest request)
    {
        try
        {
            // Validate request
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new ValidationProblemDetails(errors)
                {
                    Title = "Validation failed",
                    Status = StatusCodes.Status400BadRequest
                });
            }

            // Generate CV
            _logger.LogInformation("Generating CV for {FirstName} {LastName} with template {Template}",
                request.CVData.PersonalInfo.FirstName,
                request.CVData.PersonalInfo.LastName,
                request.Template);

            var response = await _cvService.GenerateCVAsync(request);

            _logger.LogInformation("Successfully generated CV with ID {CVId}", response.CVId);

            // Return PDF file
            return File(response.PdfContent, "application/pdf", response.FileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating CV");
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "An error occurred while generating the CV",
                    Status = StatusCodes.Status500InternalServerError
                });
        }
    }

    /// <summary>
    /// Gets a list of available CV templates
    /// </summary>
    /// <returns>List of template names</returns>
    /// <response code="200">Returns the list of available templates</response>
    [HttpGet("templates")]
    [ProducesResponseType(typeof(TemplateListResponse), StatusCodes.Status200OK)]
    public IActionResult GetTemplates()
    {
        var templates = new TemplateListResponse
        {
            Templates = new List<TemplateInfo>
            {
                new TemplateInfo
                {
                    Name = "Modern",
                    Description = "Clean and professional design with blue accent colors"
                },
                new TemplateInfo
                {
                    Name = "Classic",
                    Description = "Traditional black and white layout, perfect for conservative industries"
                },
                new TemplateInfo
                {
                    Name = "Creative",
                    Description = "Two-column layout with purple accents, ideal for creative professionals"
                }
            }
        };

        return Ok(templates);
    }

    /// <summary>
    /// Health check endpoint
    /// </summary>
    /// <returns>Status message</returns>
    [HttpGet("health")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Health()
    {
        return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }
}

/// <summary>
/// Response model for template list
/// </summary>
public class TemplateListResponse
{
    public List<TemplateInfo> Templates { get; set; } = new();
}

/// <summary>
/// Template information
/// </summary>
public class TemplateInfo
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
