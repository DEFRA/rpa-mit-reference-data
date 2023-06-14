using System.Diagnostics.CodeAnalysis;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Endpoints.RouteComponents;

/// <summary>
/// Defines methods to map minimal API endpoints to application and
/// process calls to these endpoints.
/// </summary>
public static class InvoiceTypeEndpoints
{
    private const string BasePattern = "/invoiceTypes";
    private const string EndpointTag = "InvoiceTypes";

    /// <summary>
    /// Maps scheme code endpoints to app route builder.
    /// </summary>
    /// <param name="app">Application route builder.</param>
    /// <returns>Route builder with mapped endpoints.</returns>
    [ExcludeFromCodeCoverage]
    public static IEndpointRouteBuilder MapInvoiceTypeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(BasePattern, GetInvoiceTypes)
            .Produces<IEnumerable<InvoiceType>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetInvoiceTypes")
            .WithTags(EndpointTag);

        return app;
    }

    /// <summary>
    /// Retrieves a list of valid invoice type codes.
    /// </summary>
    /// <returns>List of valid invoice type codes.</returns>
    public static async Task<IResult> GetInvoiceTypes(IInvoiceTypeDataService dataService)
    {
        var types = await dataService.Get();

        if (!types.Any())
        {
            return Results.NotFound();
        }

        return Results.Ok(types);
    }
}
