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
public class PaymentTypeDataService : IPaymentTypeDataService
{
    private readonly ReferenceDataContext _context;

    /// <summary>
    /// Creates instance of SchemeTypeDataService.
    /// </summary>
    /// <param name="context">Reference data DbContext</param>
    public PaymentTypeDataService(ReferenceDataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PaymentType>> Get(string? invoiceType = null, string? organisation = null, string? schemeType = null)
    {
        if (invoiceType is null && organisation is null && schemeType is null)
        {
            return await _context.PaymentTypes.ToListAsync();
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

        if (schemeType is not null)
        {
            query = await query.FilterBySchemeType(_context, schemeType);
        }

        var types = await query.Select(r => r.PaymentType)
            .ToArrayAsync();

        return types.DistinctBy(x => x.ComponentId);
    }
}
