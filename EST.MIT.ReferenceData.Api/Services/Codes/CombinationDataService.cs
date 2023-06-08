using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Api.Models.Responses;
using EST.MIT.ReferenceData.Data;
using EST.MIT.ReferenceData.Data.Models;
using EST.MIT.ReferenceData.Data.Models.Codes;

namespace EST.MIT.ReferenceData.Api.Services.Codes;

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
    public Task<IEnumerable<CombinationResponse>> Get(InvoiceRoute route)
    {
        var codes = _context.Combinations.Where(r => r.RouteId == route.RouteId)
            .Select(c => new CombinationResponse
            {
                AccountCode = c.AccountCode.Code,
                SchemeCode = c.SchemeCode.Code,
                DeliveryBodyCode = c.DeliveryBodyCode.Code
            })
            .AsEnumerable();

        return Task.FromResult(codes);
    }

    /// <inheritdoc />
    public Task Update(InvoiceRoute route, Combination data)
    {
        throw new NotImplementedException();
    }
}
