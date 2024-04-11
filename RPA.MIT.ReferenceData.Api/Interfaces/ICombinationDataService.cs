using RPA.MIT.ReferenceData.Api.Models.Responses;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;

namespace RPA.MIT.ReferenceData.Api.Interfaces;

/// <summary>
/// Represents a service used to perform CRUD actions on CoA combinations
/// reference data.
/// </summary>
public interface ICombinationDataService
{
    /// <summary>
    /// Adds new combination entity to specified route in data store.
    /// </summary>
    /// <param name="route">String representing invoice path.</param>
    /// <param name="data">Combination to be added.</param>
    /// <returns>Awaitable task</returns>
    Task Add(InvoiceRoute route, Combination data);

    /// <summary>
    /// Adds new combination entity collection to specified route in data store.
    /// </summary>
    /// <param name="route">String representing invoice path.</param>
    /// <param name="data">Collection of combinations to be added.</param>
    /// <returns>Awaitable task</returns>
    Task AddRange(InvoiceRoute route, IEnumerable<Combination> data);

    /// <summary>
    /// Gets combination entity from data store for a specific invoice route.
    /// </summary>
    /// <param name="route">String representing invoice path.</param>
    /// <returns>Awaitable task representing a enumerable collection of CoA combinations</returns>
    Task<IEnumerable<CombinationResponse>> Get(InvoiceRoute route);

    /// <summary>
    /// Updates given combination entity in specified route
    /// </summary>
    /// <param name="route">String representing invoice path.</param>
    /// <param name="data">Reference data entity to be updated.</param>
    /// <returns>Awaitable task</returns>
    Task Update(InvoiceRoute route, Combination data);
}
