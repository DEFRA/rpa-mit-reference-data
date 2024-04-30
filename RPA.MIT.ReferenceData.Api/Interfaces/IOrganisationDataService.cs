using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Interfaces;

/// <summary>
/// Represents a service used to perform CRUD actions on scheme types.
/// </summary>
public interface IOrganisationDataService
{
    /// <summary>
    /// Gets available organisation types. Can be filtered by invoiceType.
    /// </summary>
    /// <param name="invoiceType">Optional invoice type filter</param>
    /// <returns>Awaitable task representing a enumerable collection of organisation types</returns>
    Task<IEnumerable<Organisation>> Get(string? invoiceType = null);
}
