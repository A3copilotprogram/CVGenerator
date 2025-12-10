using CVGenerator.Domain.Interfaces;
using CVGenerator.Infrastructure.PDFGeneration;
using Microsoft.Extensions.DependencyInjection;

namespace CVGenerator.Infrastructure;

/// <summary>
/// Extension method for registering Infrastructure layer services
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Register PDF Generator service
        services.AddSingleton<ICVGeneratorService, QuestPdfGeneratorService>();

        return services;
    }
}
