using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Data;
using EST.MIT.ReferenceData.Data.Models;
using EST.MIT.ReferenceData.Data.Models.Codes;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.ReferenceData.Api.Services.Codes;

/// <summary>
/// Implementation of IReferenceDataService interface using FundCodes
/// with Entity Framework DbContext
/// </summary>
public class FundDataService : IReferenceDataService<FundCode>
{
    private readonly ReferenceDataContext _context;

    /// <summary>
    /// Creates instance of FundDataService.
    /// </summary>
    /// <param name="context">Reference data DbContext</param>
    public FundDataService(ReferenceDataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public Task Add(InvoiceRoute route, FundCode data)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task AddRange(InvoiceRoute route, IEnumerable<FundCode> data)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FundCode>> Get(InvoiceRoute route)
    {
        return await _context.InvoiceRoutes.Where(r => r.RouteId == route.RouteId)
            .Select(r => r.FundCodes)
            .SingleAsync();
    }

    /// <inheritdoc />
    public Task Update(InvoiceRoute route, FundCode data)
    {
        throw new NotImplementedException();
    }
}
