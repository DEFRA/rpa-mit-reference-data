using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Data;
using EST.MIT.ReferenceData.Data.Models.RouteComponents;

namespace EST.MIT.ReferenceData.Api.Services.RouteComponents;

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
