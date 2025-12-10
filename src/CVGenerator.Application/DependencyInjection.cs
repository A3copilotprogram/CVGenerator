using CVGenerator.Application.MappingProfiles;
using CVGenerator.Application.Services;
using CVGenerator.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CVGenerator.Application;

/// <summary>
/// Extension method for registering Application layer services
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(typeof(CVMappingProfile));

        // Register FluentValidation validators
        services.AddValidatorsFromAssemblyContaining<CreateCVRequestValidator>();

        // Register application services
        services.AddScoped<ICVService, CVService>();

        return services;
    }
}
