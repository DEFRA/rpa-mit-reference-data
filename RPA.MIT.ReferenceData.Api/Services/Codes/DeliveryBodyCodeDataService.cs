using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;

namespace RPA.MIT.ReferenceData.Api.Services.Codes;

/// <summary>
/// Implementation of IReferenceDataService interface using DeliveryBodyCode
/// with Entity Framework DbContext
/// </summary>
public class DeliveryBodyCodeDataService : IReferenceDataService<DeliveryBodyCode>
{
    private readonly ReferenceDataContext _context;

    /// <summary>
    /// Creates instance of DeliveryBodyCodeDataService.
    /// </summary>
    /// <param name="context">Reference data DbContext</param>
    public DeliveryBodyCodeDataService(ReferenceDataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public Task Add(InvoiceRoute route, DeliveryBodyCode data)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task AddRange(InvoiceRoute route, IEnumerable<DeliveryBodyCode> data)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<DeliveryBodyCode>> Get(InvoiceRoute route)
    {
        return await _context.InvoiceRoutes.Where(r => r.RouteId == route.RouteId)
            .Select(r => r.DeliveryBodyCodes)
            .SingleAsync();
    }

    /// <inheritdoc />
    public Task Update(InvoiceRoute route, DeliveryBodyCode data)
    {
        throw new NotImplementedException();
    }
}
