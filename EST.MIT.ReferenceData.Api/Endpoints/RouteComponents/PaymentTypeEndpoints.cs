using System.Diagnostics.CodeAnalysis;
using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Data.Models.RouteComponents;

namespace EST.MIT.ReferenceData.Api.Endpoints.RouteComponents;

/// <summary>
/// Defines methods to map minimal API endpoints to application and
/// process calls to these endpoints.
/// </summary>
public static class PaymentTypeEndpoints
{
    private const string BasePattern = "/paymentTypes";
    private const string EndpointTag = "PaymentTypes";

    /// <summary>
    /// Maps scheme code endpoints to app route builder.
    /// </summary>
    /// <param name="app">Application route builder.</param>
    /// <returns>Route builder with mapped endpoints.</returns>
    [ExcludeFromCodeCoverage]
    public static IEndpointRouteBuilder MapPaymentTypeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(BasePattern, GetPaymentTypes)
            .Produces<IEnumerable<PaymentType>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetPaymentTypes")
            .WithTags(EndpointTag);

        return app;
    }

    /// <summary>
    /// Retrieves a list of valid payment type codes. Can be filtered by the invoice type
    /// and / or organisation.
    /// </summary>
    /// <returns>List of valid payment type codes.</returns>
    public static async Task<IResult> GetPaymentTypes(string? invoiceType,
        string? organisation,
        string? schemeType,
        IPaymentTypeDataService dataService,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger(nameof(PaymentTypeEndpoints));

        try
        {
            var types = await dataService.Get(invoiceType, organisation, schemeType);

            if (!types.Any())
            {
                return Results.NotFound();
            }

            return Results.Ok(types);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error retrieving payment type codes: {message}", e.Message);

            return Results.BadRequest();
        }
    }
}
