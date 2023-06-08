using EST.MIT.ReferenceData.Data.Models.Codes;
using System.Diagnostics.CodeAnalysis;
using EST.MIT.ReferenceData.Api.Interfaces;

namespace EST.MIT.ReferenceData.Api.Endpoints.Codes;

/// <summary>
/// Defines methods to map minimal API endpoints to application and
/// process calls to these endpoints.
/// </summary>
public static class MarketingYearEndpoints
{
    private const string BasePattern = "/marketingYears/{invoiceType?}/{organisation?}/{schemeType?}/{paymentType?}";
    private const string EndpointTag = "MarketingYears";

    /// <summary>
    /// Maps account code endpoints to app route builder.
    /// </summary>
    /// <param name="app">Application route builder.</param>
    /// <returns>Route builder with mapped endpoints.</returns>
    [ExcludeFromCodeCoverage]
    public static IEndpointRouteBuilder MapYearEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(BasePattern, GetYears)
            .Produces<IEnumerable<AccountCode>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetMarketingYears")
            .WithTags(EndpointTag);

        return app;
    }

    /// <summary>
    /// Retrieves a list of valid marketing year codes for the given invoice path.
    /// </summary>
    /// <returns>List of valid marketing year codes.</returns>
    public static async Task<IResult> GetYears(string? invoiceType,
        string? organisation,
        string? schemeType,
        string? paymentType,
        IInvoiceRouteService routeService,
        IReferenceDataService<MarketingYearCode> dataService)
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

        var years = await dataService.Get(route);

        if (!years.Any())
        {
            return Results.NoContent();
        }

        return Results.Ok(years);
    }
}
