using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Data;
using EST.MIT.ReferenceData.Data.Models.RouteComponents;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.ReferenceData.Api.Services.RouteComponents;

/// <summary>
/// Implementation of ISchemeTypeDataService using EF DbContext as
/// data store.
/// </summary>
public class SchemeTypeDataService : ISchemeTypeDataService
{
    private readonly ReferenceDataContext _context;

    /// <summary>
    /// Creates instance of SchemeTypeDataService.
    /// </summary>
    /// <param name="context">Reference data DbContext</param>
    public SchemeTypeDataService(ReferenceDataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<SchemeType>> Get(string? invoiceType = null, string? organisation = null)
    {
        if (invoiceType is null && organisation is null)
        {
            return await _context.SchemeTypes.ToListAsync();
        }

        var query = _context.InvoiceRoutes.AsQueryable();

        if (invoiceType is not null)
        {
            query = await query.FilterByInvoiceType(_context, invoiceType);
        }

        if (organisation is not null)
        {
            query = await query.FilterByOrganisation(_context, organisation);
        }

        var types = query.Select(r => r.SchemeType)
            .ToArray();

        return types.DistinctBy(x => x.ComponentId);
    }
}
