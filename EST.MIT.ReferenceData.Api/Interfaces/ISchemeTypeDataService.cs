using EST.MIT.ReferenceData.Data.Models.RouteComponents;

namespace EST.MIT.ReferenceData.Api.Interfaces;

/// <summary>
/// Represents a service used to perform CRUD actions on scheme types.
/// </summary>
public interface ISchemeTypeDataService
{
    /// <summary>
    /// Gets available scheme types. Can be filtered by combination of invoiceType and
    /// delivery body.
    /// </summary>
    /// <param name="invoiceType">Optional invoice type filter</param>
    /// <param name="organisation">Optional organisation filter</param>
    /// <returns>Awaitable task representing a enumerable collection of scheme types</returns>
    Task<IEnumerable<SchemeType>> Get(string? invoiceType = null, string? organisation = null);
}
