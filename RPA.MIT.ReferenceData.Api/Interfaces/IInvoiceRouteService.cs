using RPA.MIT.ReferenceData.Data.Models;

namespace RPA.MIT.ReferenceData.Api.Interfaces;

/// <summary>
/// Represents a service which resolves MIT invoice routes within data store.
/// </summary>
public interface IInvoiceRouteService
{
    /// <summary>
    /// Finds route in data store according to passed parameters.
    /// </summary>
    /// <param name="invoiceType">Invoice type code</param>
    /// <param name="organisation">Organisation code</param>
    /// <param name="schemeType">Scheme type code</param>
    /// <param name="paymentType">Payment type code</param>
    /// <returns>Awaitable task representing retrieved route. Returns null if route not found.</returns>
    Task<InvoiceRoute?> GetRoute(string invoiceType, string organisation, string schemeType, string paymentType);
}
