using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Extensions;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Services.RouteComponents;

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
