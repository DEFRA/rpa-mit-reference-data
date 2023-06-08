using System.Diagnostics.CodeAnalysis;
using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Api.Models.Responses;

namespace EST.MIT.ReferenceData.Api.Endpoints.Codes;

/// <summary>
/// Defines methods to map minimal API endpoints to application and
/// process calls to these endpoints.
/// </summary>
public static class CombinationEndpoints
{
    private const string BasePattern = "/combinations/{invoiceType?}/{organisation?}/{schemeType?}/{paymentType?}";
    private const string EndpointTag = "Combinations";

    /// <summary>
    /// Maps combination endpoints to app route builder.
    /// </summary>
    /// <param name="app">Application route builder.</param>
    /// <returns>Route builder with mapped endpoints.</returns>
    [ExcludeFromCodeCoverage]
    public static IEndpointRouteBuilder MapCombinationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(BasePattern, GetCombinations)
            .Produces<IEnumerable<CombinationResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetCombinations")
            .WithTags(EndpointTag);

        return app;
    }

    /// <summary>
    /// Retrieves a list of valid account / scheme and delivery body code combinations for the given invoice path.
    /// </summary>
    /// <returns>List of valid combinations.</returns>
    /// <response code="204">No Content - All possible combinations for supplied path are valid.</response>
    public static async Task<IResult> GetCombinations(string? invoiceType,
        string? organisation,
        string? schemeType,
        string? paymentType,
        IInvoiceRouteService routeService,
        ICombinationDataService dataService)
    {
        if (invoiceType is null || organisation is null || schemeType is null || paymentType is null)
        {
            return Results.BadRequest();
        }

        var route = await routeService.GetRoute(invoiceType, organisation, schemeType, paymentType);

        if (route is null)
        {
            return Results.NotFound();
        }

        var combinations = await dataService.Get(route);

        if (!combinations.Any())
        {
            return Results.NoContent();
        }

        return Results.Ok(combinations);
    }
}
