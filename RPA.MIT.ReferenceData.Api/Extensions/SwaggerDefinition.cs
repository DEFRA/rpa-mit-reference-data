using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.OpenApi.Models;
using RPA.MIT.ReferenceData.Api.Models.ParameterFilters;

namespace RPA.MIT.ReferenceData.Api.Extensions;

/// <summary>
/// Swagger generation extension methods
/// </summary>
[ExcludeFromCodeCoverage]
public static class SwaggerDefinition
{
    /// <summary>
    /// Add and setup swagger services to application service collection
    /// </summary>
    /// <param name="services">Application service collection</param>
    /// <returns>Service collection instance with added swagger services</returns>
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "ReferenceDataApi",
                Version = "v1",
                Description = "Reference data API for manual invoice templates (MIT)"
            });

            c.ParameterFilter<InvoiceTypeParameterFilter>();
            c.ParameterFilter<OrganisationParameterFilter>();
            c.ParameterFilter<SchemeTypeParameterFilter>();
            c.ParameterFilter<PaymentTypeParameterFilter>();

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    /// <summary>
    /// Adds swagger endpoints to application builder
    /// </summary>
    /// <param name="app">Application builder</param>
    /// <returns>Application builder with added swagger endpoints</returns>
    public static IApplicationBuilder UseSwaggerEndpoints(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}