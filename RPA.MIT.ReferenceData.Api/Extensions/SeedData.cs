using System.Diagnostics.CodeAnalysis;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.SeedData;

namespace RPA.MIT.ReferenceData.Api.Extensions;

/// <summary>
/// Seed data extension methods
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Extension method that calls the SeedProvider to seed reference data
    /// into the application <see cref="ReferenceDataContext" />.
    /// </summary>
    /// <param name="app">Application builder</param>
    [ExcludeFromCodeCoverage]
    public static void UseReferenceDataSeeding(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

        using var serviceScope = scopeFactory.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        SeedProvider.SeedReferenceData(context, logger);
    }
}