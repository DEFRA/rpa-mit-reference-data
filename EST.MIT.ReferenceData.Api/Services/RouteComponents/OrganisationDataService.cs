using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Data;
using EST.MIT.ReferenceData.Data.Models.RouteComponents;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.ReferenceData.Api.Services.RouteComponents;

/// <summary>
/// Implementation of IOrganisationDataService using EF DbContext as
/// data store.
/// </summary>
public class OrganisationDataService : IOrganisationDataService
{
    private readonly ReferenceDataContext _context;

    /// <summary>
    /// Creates instance of SchemeTypeDataService.
    /// </summary>
    /// <param name="context">Reference data DbContext</param>
    public OrganisationDataService(ReferenceDataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Organisation>> Get(string? invoiceType = null)
    {
        if (invoiceType is null)
        {
            return await _context.Organisations.ToListAsync();
        }

        var query = _context.InvoiceRoutes.AsQueryable();

        if (invoiceType is not null)
        {
            query = await query.FilterByInvoiceType(_context, invoiceType);
        }

        var types = query.Select(r => r.Organisation)
            .ToArray();

        return types.DistinctBy(x => x.ComponentId);
    }
}
