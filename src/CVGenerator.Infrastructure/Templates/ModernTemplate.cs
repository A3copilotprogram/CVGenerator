using CVGenerator.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CVGenerator.Infrastructure.Templates;

/// <summary>
/// Modern CV template with clean design and blue accent colors
/// </summary>
public class ModernTemplate : IDocument
{
    private readonly CV _cv;

    public ModernTemplate(CV cv)
    {
        _cv = cv ?? throw new ArgumentNullException(nameof(cv));
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text(text =>
                {
                    text.CurrentPageNumber();
                    text.Span(" / ");
                    text.TotalPages();
                });
            });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Column(column =>
        {
            // Name
            column.Item().Background(Colors.Blue.Medium)
                .Padding(20)
                .Text($"{_cv.PersonalInfo.FirstName} {_cv.PersonalInfo.LastName}")
                .FontSize(28)
                .Bold()
                .FontColor(Colors.White);

            // Contact information
            column.Item().Padding(10).Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text($"Email: {_cv.PersonalInfo.Email}").FontSize(9);
                    if (!string.IsNullOrEmpty(_cv.PersonalInfo.PhoneNumber))
                        col.Item().Text($"Phone: {_cv.PersonalInfo.PhoneNumber}").FontSize(9);
                });

                row.RelativeItem().Column(col =>
                {
                    if (!string.IsNullOrEmpty(_cv.PersonalInfo.City))
                        col.Item().Text($"{_cv.PersonalInfo.City}, {_cv.PersonalInfo.Country}").FontSize(9);
                    if (!string.IsNullOrEmpty(_cv.PersonalInfo.LinkedIn))
                        col.Item().Text($"LinkedIn: {_cv.PersonalInfo.LinkedIn}").FontSize(9);
                });
            });

            column.Item().PaddingVertical(5).LineHorizontal(2).LineColor(Colors.Blue.Medium);
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            // Summary
            if (!string.IsNullOrEmpty(_cv.Summary))
            {
                column.Item().PaddingTop(10).Column(col =>
                {
                    col.Item().Text("PROFESSIONAL SUMMARY").Bold().FontSize(14).FontColor(Colors.Blue.Medium);
                    col.Item().PaddingTop(5).Text(_cv.Summary).FontSize(10);
                });
            }

            // Work Experience
            if (_cv.WorkExperience.Any())
            {
                column.Item().PaddingTop(15).Column(col =>
                {
                    col.Item().Text("WORK EXPERIENCE").Bold().FontSize(14).FontColor(Colors.Blue.Medium);
                    
                    foreach (var work in _cv.WorkExperience.OrderByDescending(w => w.StartDate))
                    {
                        col.Item().PaddingTop(10).Column(workCol =>
                        {
                            workCol.Item().Text(work.JobTitle).Bold().FontSize(11);
                            workCol.Item().Text($"{work.Company} | {work.Location}").FontSize(9).Italic();
                            
                            var endDate = work.IsCurrentPosition ? "Present" : work.EndDate?.ToString("MMM yyyy") ?? "Present";
                            workCol.Item().Text($"{work.StartDate:MMM yyyy} - {endDate}").FontSize(9);
                            
                            if (work.Responsibilities.Any())
                            {
                                workCol.Item().PaddingTop(5).Column(respCol =>
                                {
                                    foreach (var resp in work.Responsibilities)
                                    {
                                        respCol.Item().Row(row =>
                                        {
                                            row.ConstantItem(15).Text("•");
                                            row.RelativeItem().Text(resp).FontSize(9);
                                        });
                                    }
                                });
                            }
                        });
                    }
                });
            }

            // Education
            if (_cv.Education.Any())
            {
                column.Item().PaddingTop(15).Column(col =>
                {
                    col.Item().Text("EDUCATION").Bold().FontSize(14).FontColor(Colors.Blue.Medium);
                    
                    foreach (var edu in _cv.Education.OrderByDescending(e => e.StartDate))
                    {
                        col.Item().PaddingTop(10).Column(eduCol =>
                        {
                            eduCol.Item().Text(edu.Degree).Bold().FontSize(11);
                            eduCol.Item().Text(edu.Institution).FontSize(9).Italic();
                            
                            var endDate = edu.EndDate?.ToString("MMM yyyy") ?? "Present";
                            eduCol.Item().Text($"{edu.StartDate:MMM yyyy} - {endDate}").FontSize(9);
                            
                            if (edu.GPA.HasValue)
                                eduCol.Item().Text($"GPA: {edu.GPA:F2}").FontSize(9);
                        });
                    }
                });
            }

            // Skills
            if (_cv.Skills.Any())
            {
                column.Item().PaddingTop(15).Column(col =>
                {
                    col.Item().Text("SKILLS").Bold().FontSize(14).FontColor(Colors.Blue.Medium);
                    
                    col.Item().PaddingTop(5).Row(row =>
                    {
                        var skillsByCategory = _cv.Skills.GroupBy(s => s.Category ?? "General");
                        
                        foreach (var group in skillsByCategory)
                        {
                            row.RelativeItem().Column(catCol =>
                            {
                                catCol.Item().Text(group.Key).Bold().FontSize(10);
                                foreach (var skill in group)
                                {
                                    catCol.Item().Text($"• {skill.Name} ({skill.Level})").FontSize(9);
                                }
                            });
                        }
                    });
                });
            }

            // Certifications
            if (_cv.Certifications.Any())
            {
                column.Item().PaddingTop(15).Column(col =>
                {
                    col.Item().Text("CERTIFICATIONS").Bold().FontSize(14).FontColor(Colors.Blue.Medium);
                    
                    foreach (var cert in _cv.Certifications.OrderByDescending(c => c.IssueDate))
                    {
                        col.Item().PaddingTop(5).Column(certCol =>
                        {
                            certCol.Item().Text(cert.Name).Bold().FontSize(10);
                            certCol.Item().Text($"{cert.Issuer} - {cert.IssueDate:MMM yyyy}").FontSize(9);
                        });
                    }
                });
            }

            // Languages
            if (_cv.Languages.Any())
            {
                column.Item().PaddingTop(15).Column(col =>
                {
                    col.Item().Text("LANGUAGES").Bold().FontSize(14).FontColor(Colors.Blue.Medium);
                    col.Item().PaddingTop(5).Text(string.Join(", ", _cv.Languages)).FontSize(10);
                });
            }
        });
    }
}
