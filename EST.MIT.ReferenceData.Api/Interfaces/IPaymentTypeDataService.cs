using EST.MIT.ReferenceData.Data.Models.RouteComponents;

namespace EST.MIT.ReferenceData.Api.Interfaces;

/// <summary>
/// Represents a service used to perform CRUD actions on payment types.
/// </summary>
public interface IPaymentTypeDataService
{
    /// <summary>
    /// Gets available scheme types. Can be filtered by combination of invoiceType and
    /// delivery body.
    /// </summary>
    /// <param name="invoiceType">Optional invoice type filter</param>
    /// <param name="organisation">Optional organisation filter</param>
    /// <param name="schemeType">Optional payment type filter</param>
    /// <returns>Awaitable task representing a enumerable collection of payment types</returns>
    Task<IEnumerable<PaymentType>> Get(string? invoiceType = null, string? organisation = null, string? schemeType = null);
}
