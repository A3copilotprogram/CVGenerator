using CVGenerator.Domain.Entities;
using CVGenerator.Domain.Interfaces;
using CVGenerator.Infrastructure.Templates;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace CVGenerator.Infrastructure.PDFGeneration;

/// <summary>
/// PDF generator service using QuestPDF
/// </summary>
public class QuestPdfGeneratorService : ICVGeneratorService
{
    public QuestPdfGeneratorService()
    {
        // Configure QuestPDF license (Community license is free for non-commercial use)
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public async Task<byte[]> GeneratePdfAsync(CV cv, CVTemplate template)
    {
        return await Task.Run(() =>
        {
            IDocument document = template switch
            {
                CVTemplate.Modern => new ModernTemplate(cv),
                CVTemplate.Classic => new ClassicTemplate(cv),
                CVTemplate.Creative => new CreativeTemplate(cv),
                _ => new ModernTemplate(cv)
            };

            return document.GeneratePdf();
        });
    }
}
