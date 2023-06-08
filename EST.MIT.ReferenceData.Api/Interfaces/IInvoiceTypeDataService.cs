using EST.MIT.ReferenceData.Data.Models.RouteComponents;

namespace EST.MIT.ReferenceData.Api.Interfaces;

/// <summary>
/// Represents a service used to perform CRUD actions on invoice types.
/// </summary>
public interface IInvoiceTypeDataService
{
    /// <summary>
    /// Gets available invoice types.
    /// </summary>
    /// <returns>Awaitable task representing a enumerable collection of invoice types</returns>
    Task<IEnumerable<InvoiceType>> Get();
}
