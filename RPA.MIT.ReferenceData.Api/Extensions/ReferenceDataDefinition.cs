using System.Diagnostics.CodeAnalysis;
using RPA.MIT.ReferenceData.Api.Endpoints.Codes;
using RPA.MIT.ReferenceData.Api.Endpoints.RouteComponents;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Api.Services;
using RPA.MIT.ReferenceData.Api.Services.Codes;
using RPA.MIT.ReferenceData.Api.Services.RouteComponents;
using RPA.MIT.ReferenceData.Data.Models.Codes;

namespace RPA.MIT.ReferenceData.Api.Extensions;

/// <summary>
/// Reference data service registration extensions
/// </summary>
[ExcludeFromCodeCoverage]
public static class ReferenceDataDefinition
{
    /// <summary>
    /// Add reference data service to application service collection
    /// </summary>
    /// <param name="services">Application service collection</param>
    /// <returns>Service collection instance with added reference data services</returns>
    public static IServiceCollection AddReferenceDataServices(this IServiceCollection services)
    {
        services.AddScoped<IInvoiceRouteService, InvoiceRouteService>();
        services.AddScoped<IReferenceDataService<AccountCode>, AccountDataService>();
        services.AddScoped<IReferenceDataService<SchemeCode>, SchemeDataService>();
        services.AddScoped<IReferenceDataService<FundCode>, FundDataService>();
        services.AddScoped<IReferenceDataService<DeliveryBodyCode>, DeliveryBodyCodeDataService>();
        services.AddScoped<IReferenceDataService<MarketingYearCode>, MarketingYearDataService>();
        services.AddScoped<ICombinationDataService, CombinationDataService>();

        services.AddScoped<IInvoiceTypeDataService, InvoiceTypeDataService>();
        services.AddScoped<IOrganisationDataService, OrganisationDataService>();
        services.AddScoped<ISchemeTypeDataService, SchemeTypeDataService>();
        services.AddScoped<IPaymentTypeDataService, PaymentTypeDataService>();

        return services;
    }

    /// <summary>
    /// Maps reference data endpoints to application.
    /// </summary>
    /// <param name="app">Application route builder.</param>
    /// <returns>Route builder with mapped endpoints.</returns>
    public static IEndpointRouteBuilder UseReferenceDataEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAccountEndpoints();
        app.MapSchemeEndpoints();
        app.MapFundEndpoints();
        app.MapDeliveryBodyEndpoints();
        app.MapYearEndpoints();
        app.MapCombinationEndpoints();

        app.MapInvoiceTypeEndpoints();
        app.MapOrganisationEndpoints();
        app.MapSchemeTypeEndpoints();
        app.MapPaymentTypeEndpoints();

        return app;
    }
}
