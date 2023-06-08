using System.Diagnostics.CodeAnalysis;
using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Data.Models.RouteComponents;

namespace EST.MIT.ReferenceData.Api.Endpoints.RouteComponents;

/// <summary>
/// Defines methods to map minimal API endpoints to application and
/// process calls to these endpoints.
/// </summary>
public static class OrganisationEndpoints
{
    private const string BasePattern = "/organisations";
    private const string EndpointTag = "Organisations";

    /// <summary>
    /// Maps scheme code endpoints to app route builder.
    /// </summary>
    /// <param name="app">Application route builder.</param>
    /// <returns>Route builder with mapped endpoints.</returns>
    [ExcludeFromCodeCoverage]
    public static IEndpointRouteBuilder MapOrganisationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(BasePattern, GetOrganisations)
            .Produces<IEnumerable<Organisation>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetOrganisations")
            .WithTags(EndpointTag);

        return app;
    }

    /// <summary>
    /// Retrieves a list of valid organisation codes. Can be filtered by the invoice type.
    /// </summary>
    /// <returns>List of valid organisation codes.</returns>
    public static async Task<IResult> GetOrganisations(
        string? invoiceType,
        IOrganisationDataService dataService,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(nameof(OrganisationEndpoints));

        try
        {
            var types = await dataService.Get(invoiceType);

            if (!types.Any())
            {
                return Results.NotFound();
            }

            return Results.Ok(types);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error retrieving organisation type codes: {message}", e.Message);

            return Results.BadRequest();
        }
    }
}
