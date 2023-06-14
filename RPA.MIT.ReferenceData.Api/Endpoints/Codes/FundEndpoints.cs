using System.Diagnostics.CodeAnalysis;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data.Models.Codes;

namespace RPA.MIT.ReferenceData.Api.Endpoints.Codes;

/// <summary>
/// Defines methods to map minimal API endpoints to application and
/// process calls to these endpoints.
/// </summary>
public static class FundEndpoints
{
    private const string BasePattern = "/funds/{invoiceType?}/{organisation?}/{schemeType?}/{paymentType?}";
    private const string EndpointTag = "Funds";

    /// <summary>
    /// Maps fund code endpoints to app route builder.
    /// </summary>
    /// <param name="app">Application route builder.</param>
    /// <returns>Route builder with mapped endpoints.</returns>
    [ExcludeFromCodeCoverage]
    public static IEndpointRouteBuilder MapFundEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(BasePattern, GetFunds)
            .Produces<IEnumerable<FundCode>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetFunds")
            .WithTags(EndpointTag);

        return app;
    }

    /// <summary>
    /// Retrieves a list of valid fund codes for the given invoice path.
    /// </summary>
    /// <returns>List of valid fund codes.</returns>
    public static async Task<IResult> GetFunds(string? invoiceType,
        string? organisation,
        string? schemeType,
        string? paymentType,
        IInvoiceRouteService routeService,
        IReferenceDataService<FundCode> dataService)
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

        var funds = await dataService.Get(route);

        if (!funds.Any())
        {
            return Results.NoContent();
        }

        return Results.Ok(funds);
    }
}
