using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Services.RouteComponents;

/// <summary>
/// Implementation of IInvoiceTypeDataService using EF DbContext as
/// data store.
/// </summary>
public class InvoiceTypeDataService : IInvoiceTypeDataService
{
    private readonly ReferenceDataContext _context;

    /// <summary>
    /// Creates instance of InvoiceTypeDataService.
    /// </summary>
    /// <param name="context">Reference data DbContext</param>
    public InvoiceTypeDataService(ReferenceDataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public Task<IEnumerable<InvoiceType>> Get()
    {
        var types = _context.InvoiceTypes.ToArray();

        return Task.FromResult(types.DistinctBy(x => x.ComponentId));
    }
}
