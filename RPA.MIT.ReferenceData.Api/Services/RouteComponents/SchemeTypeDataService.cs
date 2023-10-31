using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Extensions;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Services.RouteComponents;

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

        var types = await query.Select(r => r.SchemeType).ToArrayAsync();

        return types.DistinctBy(x => x.ComponentId);
    }
}
