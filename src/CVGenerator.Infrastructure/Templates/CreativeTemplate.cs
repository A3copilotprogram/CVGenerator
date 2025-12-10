using CVGenerator.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CVGenerator.Infrastructure.Templates;

/// <summary>
/// Creative CV template with modern two-column layout and purple accent colors
/// </summary>
public class CreativeTemplate : IDocument
{
    private readonly CV _cv;

    public CreativeTemplate(CV cv)
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
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text(text =>
                {
                    text.CurrentPageNumber();
                    text.Span(" / ");
                    text.TotalPages();
                    text.Span(" ").FontSize(8);
                });
            });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Background(Colors.Purple.Darken3)
            .Padding(20)
            .Column(column =>
            {
                column.Item().Text($"{_cv.PersonalInfo.FirstName} {_cv.PersonalInfo.LastName}")
                    .FontSize(32)
                    .Bold()
                    .FontColor(Colors.White);

                column.Item().PaddingTop(5).Row(row =>
                {
                    if (!string.IsNullOrEmpty(_cv.PersonalInfo.Email))
                    {
                        row.AutoItem().Text("✉ " + _cv.PersonalInfo.Email)
                            .FontSize(9)
                            .FontColor(Colors.White);
                        row.AutoItem().PaddingLeft(15);
                    }

                    if (!string.IsNullOrEmpty(_cv.PersonalInfo.PhoneNumber))
                    {
                        row.AutoItem().Text("☎ " + _cv.PersonalInfo.PhoneNumber)
                            .FontSize(9)
                            .FontColor(Colors.White);
                        row.AutoItem().PaddingLeft(15);
                    }

                    if (!string.IsNullOrEmpty(_cv.PersonalInfo.LinkedIn))
                    {
                        row.AutoItem().Text("in " + _cv.PersonalInfo.LinkedIn)
                            .FontSize(9)
                            .FontColor(Colors.White);
                    }
                });
            });
    }

    private void ComposeContent(IContainer container)
    {
        container.PaddingTop(10).Row(row =>
        {
            // Left Column (Sidebar)
            row.ConstantItem(180).Background(Colors.Grey.Lighten3)
                .Padding(15)
                .Column(leftColumn =>
                {
                    // Skills
                    if (_cv.Skills.Any())
                    {
                        leftColumn.Item().Column(col =>
                        {
                            col.Item().Text("SKILLS")
                                .Bold()
                                .FontSize(13)
                                .FontColor(Colors.Purple.Darken3);
                            
                            col.Item().PaddingTop(2).LineHorizontal(2).LineColor(Colors.Purple.Darken3);
                            
                            col.Item().PaddingTop(8).Column(skillCol =>
                            {
                                var skillsByCategory = _cv.Skills.GroupBy(s => s.Category ?? "General");
                                
                                foreach (var group in skillsByCategory)
                                {
                                    skillCol.Item().PaddingBottom(5).Column(catCol =>
                                    {
                                        catCol.Item().Text(group.Key)
                                            .Bold()
                                            .FontSize(10)
                                            .FontColor(Colors.Purple.Darken2);
                                        
                                        foreach (var skill in group)
                                        {
                                            catCol.Item().PaddingTop(2).Row(skillRow =>
                                            {
                                                skillRow.AutoItem().Text("•").FontSize(8);
                                                skillRow.AutoItem().PaddingLeft(5);
                                                skillRow.RelativeItem().Text(skill.Name).FontSize(9);
                                            });
                                        }
                                    });
                                }
                            });
                        });
                    }

                    // Languages
                    if (_cv.Languages.Any())
                    {
                        leftColumn.Item().PaddingTop(15).Column(col =>
                        {
                            col.Item().Text("LANGUAGES")
                                .Bold()
                                .FontSize(13)
                                .FontColor(Colors.Purple.Darken3);
                            
                            col.Item().PaddingTop(2).LineHorizontal(2).LineColor(Colors.Purple.Darken3);
                            
                            col.Item().PaddingTop(8).Column(langCol =>
                            {
                                foreach (var lang in _cv.Languages)
                                {
                                    langCol.Item().PaddingTop(2).Text($"• {lang}").FontSize(9);
                                }
                            });
                        });
                    }

                    // Certifications
                    if (_cv.Certifications.Any())
                    {
                        leftColumn.Item().PaddingTop(15).Column(col =>
                        {
                            col.Item().Text("CERTIFICATIONS")
                                .Bold()
                                .FontSize(13)
                                .FontColor(Colors.Purple.Darken3);
                            
                            col.Item().PaddingTop(2).LineHorizontal(2).LineColor(Colors.Purple.Darken3);
                            
                            col.Item().PaddingTop(8).Column(certCol =>
                            {
                                foreach (var cert in _cv.Certifications.OrderByDescending(c => c.IssueDate))
                                {
                                    certCol.Item().PaddingTop(5).Column(c =>
                                    {
                                        c.Item().Text(cert.Name).Bold().FontSize(9);
                                        c.Item().Text(cert.Issuer).FontSize(8).Italic();
                                        c.Item().Text(cert.IssueDate.ToString("MMM yyyy")).FontSize(8);
                                    });
                                }
                            });
                        });
                    }
                });

            row.RelativeItem().PaddingLeft(15).Column(rightColumn =>
            {
                // Summary
                if (!string.IsNullOrEmpty(_cv.Summary))
                {
                    rightColumn.Item().Column(col =>
                    {
                        col.Item().Text("PROFILE")
                            .Bold()
                            .FontSize(14)
                            .FontColor(Colors.Purple.Darken3);
                        
                        col.Item().PaddingTop(2).LineHorizontal(2).LineColor(Colors.Purple.Darken3);
                        col.Item().PaddingTop(8).Text(_cv.Summary).FontSize(10).LineHeight(1.5f);
                    });
                }

                // Work Experience
                if (_cv.WorkExperience.Any())
                {
                    rightColumn.Item().PaddingTop(15).Column(col =>
                    {
                        col.Item().Text("EXPERIENCE")
                            .Bold()
                            .FontSize(14)
                            .FontColor(Colors.Purple.Darken3);
                        
                        col.Item().PaddingTop(2).LineHorizontal(2).LineColor(Colors.Purple.Darken3);
                        
                        foreach (var work in _cv.WorkExperience.OrderByDescending(w => w.StartDate))
                        {
                            col.Item().PaddingTop(10).Column(workCol =>
                            {
                                workCol.Item().Text(work.JobTitle)
                                    .Bold()
                                    .FontSize(12)
                                    .FontColor(Colors.Purple.Darken2);
                                
                                workCol.Item().Text($"{work.Company} | {work.Location}")
                                    .FontSize(10)
                                    .Italic();
                                
                                var endDate = work.IsCurrentPosition ? "Present" : work.EndDate?.ToString("MMM yyyy") ?? "Present";
                                workCol.Item().Text($"{work.StartDate:MMM yyyy} - {endDate}")
                                    .FontSize(9)
                                    .FontColor(Colors.Grey.Darken1);
                                
                                if (work.Responsibilities.Any())
                                {
                                    workCol.Item().PaddingTop(5).Column(respCol =>
                                    {
                                        foreach (var resp in work.Responsibilities)
                                        {
                                            respCol.Item().PaddingTop(2).Row(respRow =>
                                            {
                                                respRow.ConstantItem(15).Text("▸").FontColor(Colors.Purple.Darken3);
                                                respRow.RelativeItem().Text(resp).FontSize(9).LineHeight(1.4f);
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
                    rightColumn.Item().PaddingTop(15).Column(col =>
                    {
                        col.Item().Text("EDUCATION")
                            .Bold()
                            .FontSize(14)
                            .FontColor(Colors.Purple.Darken3);
                        
                        col.Item().PaddingTop(2).LineHorizontal(2).LineColor(Colors.Purple.Darken3);
                        
                        foreach (var edu in _cv.Education.OrderByDescending(e => e.StartDate))
                        {
                            col.Item().PaddingTop(10).Column(eduCol =>
                            {
                                eduCol.Item().Text(edu.Degree)
                                    .Bold()
                                    .FontSize(11)
                                    .FontColor(Colors.Purple.Darken2);
                                
                                eduCol.Item().Text(edu.Institution)
                                    .FontSize(10)
                                    .Italic();
                                
                                var endDate = edu.EndDate?.ToString("MMM yyyy") ?? "Present";
                                eduCol.Item().Text($"{edu.StartDate:MMM yyyy} - {endDate}")
                                    .FontSize(9)
                                    .FontColor(Colors.Grey.Darken1);
                                
                                if (edu.GPA.HasValue)
                                    eduCol.Item().Text($"GPA: {edu.GPA:F2}").FontSize(9);
                            });
                        }
                    });
                }
            });
        });
    }
}
