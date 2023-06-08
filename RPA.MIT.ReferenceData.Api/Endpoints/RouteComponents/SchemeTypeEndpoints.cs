using System.Diagnostics.CodeAnalysis;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Endpoints.RouteComponents;

/// <summary>
/// Defines methods to map minimal API endpoints to application and
/// process calls to these endpoints.
/// </summary>
public static class SchemeTypeEndpoints
{
    private const string BasePattern = "/schemeTypes";
    private const string EndpointTag = "SchemeTypes";

    /// <summary>
    /// Maps scheme code endpoints to app route builder.
    /// </summary>
    /// <param name="app">Application route builder.</param>
    /// <returns>Route builder with mapped endpoints.</returns>
    [ExcludeFromCodeCoverage]
    public static IEndpointRouteBuilder MapSchemeTypeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(BasePattern, GetSchemeTypes)
            .Produces<IEnumerable<SchemeType>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetSchemeTypes")
            .WithTags(EndpointTag);

        return app;
    }

    /// <summary>
    /// Retrieves a list of valid scheme type codes. Can be filtered by the invoice type
    /// and / or organisation.
    /// </summary>
    /// <returns>List of valid scheme type codes.</returns>
    public static async Task<IResult> GetSchemeTypes(string? invoiceType,
        string? organisation,
        ISchemeTypeDataService dataService,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(nameof(SchemeTypeEndpoints));

        try
        {
            var types = await dataService.Get(invoiceType, organisation);

            if (!types.Any())
            {
                return Results.NotFound();
            }

            return Results.Ok(types);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error retrieving scheme type codes: {message}", e.Message);

            return Results.BadRequest();
        }
    }
}
