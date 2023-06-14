using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;

namespace RPA.MIT.ReferenceData.Api.Services.Codes;

/// <summary>
/// Implementation of IReferenceDataService interface using MarketingYearCode
/// with Entity Framework DbContext
/// </summary>
public class MarketingYearDataService : IReferenceDataService<MarketingYearCode>
{
    private readonly ReferenceDataContext _context;

    /// <summary>
    /// Creates instance of MarketingYearDataService.
    /// </summary>
    /// <param name="context">Reference data DbContext</param>
    public MarketingYearDataService(ReferenceDataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public Task Add(InvoiceRoute route, MarketingYearCode data)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task AddRange(InvoiceRoute route, IEnumerable<MarketingYearCode> data)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<MarketingYearCode>> Get(InvoiceRoute route)
    {
        var codes = _context.InvoiceRoutes.Where(r => r.RouteId == route.RouteId)
            .Select(r => r.MarketingYearCodes)
            .Single()
            .AsEnumerable();

        return Task.FromResult(codes);
    }

    /// <inheritdoc />
    public Task Update(InvoiceRoute route, MarketingYearCode data)
    {
        throw new NotImplementedException();
    }
}
