using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Api.Services.Codes;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Test.Services.Codes;

public class FundDataServiceTests
{
    private readonly IReferenceDataService<FundCode> _fundDataService;
    private readonly ReferenceDataContext _context;

    public FundDataServiceTests()
    {
        _context = Utilites.GetRefDataContext();

        _fundDataService = new FundDataService(_context);

        _context.InvoiceTypes.Add(new InvoiceType("AP", "Accounts Payable"));
        _context.Organisations.Add(new Organisation("EA", "Environment Agency"));
        _context.SchemeTypes.Add(new SchemeType("CS", "CS"));
        _context.PaymentTypes.Add(new PaymentType("EU", "European Union"));

        _context.FundCodes.AddRange(
            new FundCode("EXQ00"),
            new FundCode("DOM00"),
            new FundCode("DOM10"),
            new FundCode("DRD00")
        );

        _context.SaveChanges();
    }

    [Fact]
    public async Task GetShouldReturnValidFundCodes()
    {
        // Arrange
        var mockRoute = new InvoiceRoute(
            _context.InvoiceTypes.Single(x => x.Code == "AP"),
            _context.Organisations.Single(x => x.Code == "EA"),
            _context.SchemeTypes.Single(x => x.Code == "CS"),
            _context.PaymentTypes.Single(x => x.Code == "EU")
        );

        await _context.InvoiceRoutes.AddAsync(mockRoute);

        await _context.SaveChangesAsync();

        var route = _context.InvoiceRoutes.Include(r => r.FundCodes)
            .Single(r => r.InvoiceType.Code == "AP"
                        && r.Organisation.Code == "EA"
                        && r.SchemeType.Code == "CS"
                        && r.PaymentType.Code == "EU");

        route.FundCodes.Add(_context.FundCodes.Single(x => x.Code == "EXQ00"));
        route.FundCodes.Add(_context.FundCodes.Single(x => x.Code == "DOM10"));
        route.FundCodes.Add(_context.FundCodes.Single(x => x.Code == "DRD00"));

        await _context.SaveChangesAsync();

        // Act
        var codes = (await _fundDataService.Get(route)).ToArray();

        // Assert
        Assert.NotNull(codes);
        Assert.Equal(3, codes.Length);
        Assert.Equal("EXQ00", codes[0].Code);
        Assert.Equal("DOM10", codes[1].Code);
        Assert.Equal("DRD00", codes[2].Code);
    }

    [Fact]
    public async Task AddShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _fundDataService.Add(null!, null!));
    }

    [Fact]
    public async Task AddRangeShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _fundDataService.AddRange(null!, null!));
    }

    [Fact]
    public async Task UpdateShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _fundDataService.Update(null!, null!));
    }
}