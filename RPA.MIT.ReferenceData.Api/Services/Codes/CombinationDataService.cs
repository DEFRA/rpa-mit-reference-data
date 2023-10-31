using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Api.Models.Responses;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;

namespace RPA.MIT.ReferenceData.Api.Services.Codes;

/// <summary>
/// Implementation of ICombinationDataService using Entity Framework DbContext
/// </summary>
public class CombinationDataService : ICombinationDataService
{
    private readonly ReferenceDataContext _context;

    /// <summary>
    /// Creates instance of AccountDataService.
    /// </summary>
    /// <param name="context">Reference data DbContext</param>
    public CombinationDataService(ReferenceDataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public Task Add(InvoiceRoute route, Combination data)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task AddRange(InvoiceRoute route, IEnumerable<Combination> data)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CombinationResponse>> Get(InvoiceRoute route)
    {
        return await _context.Combinations.Where(r => r.RouteId == route.RouteId)
            .Select(c => new CombinationResponse
            {
                AccountCode = c.AccountCode.Code,
                SchemeCode = c.SchemeCode.Code,
                DeliveryBodyCode = c.DeliveryBodyCode.Code
            }).ToArrayAsync();
    }

    /// <inheritdoc />
    public Task Update(InvoiceRoute route, Combination data)
    {
        throw new NotImplementedException();
    }
}
