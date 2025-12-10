using CVGenerator.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CVGenerator.Infrastructure.Templates;

/// <summary>
/// Classic CV template with traditional black and white design
/// </summary>
public class ClassicTemplate : IDocument
{
    private readonly CV _cv;

    public ClassicTemplate(CV cv)
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
                page.Margin(50);  // Wider margin for classic formal look
                page.DefaultTextStyle(x => x.FontSize(TemplateConstants.StandardFontSize).FontFamily("Times New Roman"));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Page ");
                    text.CurrentPageNumber();
                    text.Span(" of ");
                    text.TotalPages();
                });
            });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Column(column =>
        {
            // Name - Centered
            column.Item().AlignCenter()
                .Text($"{_cv.PersonalInfo.FirstName} {_cv.PersonalInfo.LastName}")
                .FontSize(24)
                .Bold();

            // Contact Information - Centered
            column.Item().PaddingTop(5).AlignCenter().Column(col =>
            {
                var contactParts = new List<string>();
                
                if (!string.IsNullOrEmpty(_cv.PersonalInfo.Address))
                    contactParts.Add(_cv.PersonalInfo.Address);
                
                if (!string.IsNullOrEmpty(_cv.PersonalInfo.City))
                    contactParts.Add($"{_cv.PersonalInfo.City}, {_cv.PersonalInfo.Country}");
                
                if (contactParts.Any())
                    col.Item().Text(string.Join(" | ", contactParts)).FontSize(9);

                var contactLine = new List<string>
                {
                    _cv.PersonalInfo.Email
                };
                
                if (!string.IsNullOrEmpty(_cv.PersonalInfo.PhoneNumber))
                    contactLine.Add(_cv.PersonalInfo.PhoneNumber);
                
                col.Item().Text(string.Join(" | ", contactLine)).FontSize(9);
            });

            column.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Black);
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            // Summary / Objective
            if (!string.IsNullOrEmpty(_cv.Summary))
            {
                column.Item().PaddingTop(15).Column(col =>
                {
                    col.Item().Text("OBJECTIVE").Bold().FontSize(12).Underline();
                    col.Item().PaddingTop(5).Text(_cv.Summary).FontSize(10).LineHeight(1.5f);
                });
            }

            // Education
            if (_cv.Education.Any())
            {
                column.Item().PaddingTop(15).Column(col =>
                {
                    col.Item().Text("EDUCATION").Bold().FontSize(12).Underline();
                    
                    foreach (var edu in _cv.Education.OrderByDescending(e => e.StartDate))
                    {
                        col.Item().PaddingTop(10).Column(eduCol =>
                        {
                            eduCol.Item().Row(row =>
                            {
                                row.RelativeItem().Text(edu.Degree).Bold().FontSize(11);
                                var endDate = edu.EndDate?.ToString("yyyy") ?? "Present";
                                row.ConstantItem(100).AlignRight().Text($"{edu.StartDate:yyyy} - {endDate}").FontSize(10);
                            });
                            
                            eduCol.Item().Text(edu.Institution).Italic().FontSize(10);
                            
                            if (!string.IsNullOrEmpty(edu.Location))
                                eduCol.Item().Text(edu.Location).FontSize(9);
                            
                            if (edu.GPA.HasValue)
                                eduCol.Item().Text($"GPA: {edu.GPA:F2}").FontSize(9);
                            
                            if (!string.IsNullOrEmpty(edu.Description))
                                eduCol.Item().PaddingTop(3).Text(edu.Description).FontSize(9).LineHeight(1.4f);
                        });
                    }
                });
            }

            // Work Experience
            if (_cv.WorkExperience.Any())
            {
                column.Item().PaddingTop(15).Column(col =>
                {
                    col.Item().Text("PROFESSIONAL EXPERIENCE").Bold().FontSize(12).Underline();
                    
                    foreach (var work in _cv.WorkExperience.OrderByDescending(w => w.StartDate))
                    {
                        col.Item().PaddingTop(10).Column(workCol =>
                        {
                            workCol.Item().Row(row =>
                            {
                                row.RelativeItem().Text(work.JobTitle).Bold().FontSize(11);
                                var endDate = work.IsCurrentPosition ? "Present" : work.EndDate?.ToString("MMM yyyy") ?? "Present";
                                row.ConstantItem(120).AlignRight().Text($"{work.StartDate:MMM yyyy} - {endDate}").FontSize(10);
                            });
                            
                            workCol.Item().Text($"{work.Company}, {work.Location}").Italic().FontSize(10);
                            
                            if (work.Responsibilities.Any())
                            {
                                workCol.Item().PaddingTop(5).Column(respCol =>
                                {
                                    foreach (var resp in work.Responsibilities)
                                    {
                                        respCol.Item().PaddingTop(2).Row(row =>
                                        {
                                            row.ConstantItem(20).Text("•");
                                            row.RelativeItem().Text(resp).FontSize(9).LineHeight(1.4f);
                                        });
                                    }
                                });
                            }
                            
                            if (work.Achievements.Any())
                            {
                                workCol.Item().PaddingTop(3).Column(achCol =>
                                {
                                    achCol.Item().Text("Key Achievements:").Bold().FontSize(9);
                                    foreach (var ach in work.Achievements)
                                    {
                                        achCol.Item().PaddingTop(2).Row(row =>
                                        {
                                            row.ConstantItem(20).Text("○");
                                            row.RelativeItem().Text(ach).FontSize(9).LineHeight(1.4f);
                                        });
                                    }
                                });
                            }
                        });
                    }
                });
            }

            // Skills
            if (_cv.Skills.Any())
            {
                column.Item().PaddingTop(15).Column(col =>
                {
                    col.Item().Text("SKILLS").Bold().FontSize(12).Underline();
                    
                    var skillsByCategory = _cv.Skills.GroupBy(s => s.Category ?? "General");
                    
                    foreach (var group in skillsByCategory)
                    {
                        col.Item().PaddingTop(5).Row(row =>
                        {
                            row.ConstantItem(120).Text($"{group.Key}:").Bold().FontSize(10);
                            row.RelativeItem().Text(string.Join(", ", group.Select(s => s.Name))).FontSize(10);
                        });
                    }
                });
            }

            // Certifications
            if (_cv.Certifications.Any())
            {
                column.Item().PaddingTop(15).Column(col =>
                {
                    col.Item().Text("CERTIFICATIONS").Bold().FontSize(12).Underline();
                    
                    foreach (var cert in _cv.Certifications.OrderByDescending(c => c.IssueDate))
                    {
                        col.Item().PaddingTop(5).Row(row =>
                        {
                            row.RelativeItem().Text($"• {cert.Name}").FontSize(10);
                            row.ConstantItem(100).AlignRight().Text(cert.IssueDate.ToString("MMM yyyy")).FontSize(9);
                        });
                        col.Item().Text($"  {cert.Issuer}").FontSize(9).Italic();
                    }
                });
            }

            // Languages
            if (_cv.Languages.Any())
            {
                column.Item().PaddingTop(15).Column(col =>
                {
                    col.Item().Text("LANGUAGES").Bold().FontSize(12).Underline();
                    col.Item().PaddingTop(5).Text(string.Join(", ", _cv.Languages)).FontSize(10);
                });
            }
        });
    }
}
