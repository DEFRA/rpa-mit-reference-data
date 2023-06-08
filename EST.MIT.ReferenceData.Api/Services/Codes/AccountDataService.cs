using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Data;
using EST.MIT.ReferenceData.Data.Models;
using EST.MIT.ReferenceData.Data.Models.Codes;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.ReferenceData.Api.Services.Codes;

/// <summary>
/// Implementation of IReferenceDataService interface using AccountCodes
/// with Entity Framework DbContext
/// </summary>
public class AccountDataService : IReferenceDataService<AccountCode>
{
    private readonly ReferenceDataContext _context;

    /// <summary>
    /// Creates instance of AccountDataService.
    /// </summary>
    /// <param name="context">Reference data DbContext</param>
    public AccountDataService(ReferenceDataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public Task Add(InvoiceRoute route, AccountCode data)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task AddRange(InvoiceRoute route, IEnumerable<AccountCode> data)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AccountCode>> Get(InvoiceRoute route)
    {
        return await _context.InvoiceRoutes.Where(r => r.RouteId == route.RouteId)
            .Select(r => r.AccountCodes)
            .SingleAsync();
    }

    /// <inheritdoc />
    public Task Update(InvoiceRoute route, AccountCode data)
    {
        throw new NotImplementedException();
    }
}
