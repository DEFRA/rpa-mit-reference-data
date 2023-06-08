using EST.MIT.ReferenceData.Data.Models;
using EST.MIT.ReferenceData.Data.Models.Codes;

namespace EST.MIT.ReferenceData.Api.Interfaces;

/// <summary>
/// Represents a service used to perform CRUD actions on reference
/// data.
/// </summary>
/// <typeparam name="TEntity">Reference data type processed by implementation.</typeparam>
public interface IReferenceDataService<TEntity> where TEntity : CodeBase
{
    /// <summary>
    /// Adds new reference data entity to specified route in data store.
    /// </summary>
    /// <param name="route">String representing invoice path</param>
    /// <param name="data">Reference data entity to be added.</param>
    /// <returns>Awaitable task</returns>
    Task Add(InvoiceRoute route, TEntity data);

    /// <summary>
    /// Adds new reference data collection to specified route in data store.
    /// </summary>
    /// <param name="route">String representing invoice path</param>
    /// <param name="data">Collection of reference data entities to be added.</param>
    /// <returns>Awaitable task</returns>
    Task AddRange(InvoiceRoute route, IEnumerable<TEntity> data);

    /// <summary>
    /// Gets reference data from data store for a specific invoice route.
    /// </summary>
    /// <param name="route">String representing invoice path</param>
    /// <returns>Awaitable task representing a enumerable collection ref data entity</returns>
    Task<IEnumerable<TEntity>> Get(InvoiceRoute route);

    /// <summary>
    /// Updates given data entity in specified route
    /// </summary>
    /// <param name="route">String representing invoice path</param>
    /// <param name="data">Reference data entity to be updated.</param>
    /// <returns>Awaitable task</returns>
    Task Update(InvoiceRoute route, TEntity data);
}
