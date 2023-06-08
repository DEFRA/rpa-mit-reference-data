using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Data;
using EST.MIT.ReferenceData.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.ReferenceData.Api.Services;

/// <summary>
/// Implementation of IInvoiceRouteService using Entity Framework DbContext
/// as data store.
/// </summary>
public class InvoiceRouteService : IInvoiceRouteService
{
    private readonly ReferenceDataContext _context;

    /// <summary>
    /// Creates instance of InvoiceRouteService.
    /// </summary>
    /// <param name="context">Reference data DbContext</param>
    public InvoiceRouteService(ReferenceDataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<InvoiceRoute?> GetRoute(string invoiceType,
        string organisation,
        string schemeType,
        string paymentType)
    {
        var route = await _context.InvoiceRoutes.SingleOrDefaultAsync(r => r.InvoiceType.Code == invoiceType
            && r.Organisation.Code == organisation
            && r.SchemeType.Code == schemeType
            && r.PaymentType.Code == paymentType);

        return route;
    }
}
