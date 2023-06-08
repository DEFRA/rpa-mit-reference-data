using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Api.Services.Codes;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Test.Services.Codes;

public class MarketingYearDataServiceTests
{
    private readonly IReferenceDataService<MarketingYearCode> _yearDataService;
    private readonly ReferenceDataContext _context;

    public MarketingYearDataServiceTests()
    {
        _context = Utilites.GetRefDataContext();

        _yearDataService = new MarketingYearDataService(_context);

        _context.InvoiceTypes.Add(new InvoiceType("AP", "Accounts Payable"));
        _context.Organisations.Add(new Organisation("EA", "Environment Agency"));
        _context.SchemeTypes.Add(new SchemeType("CS", "CS"));
        _context.PaymentTypes.Add(new PaymentType("EU", "European Union"));

        _context.MarketingYearCodes.AddRange(
            new MarketingYearCode("2022", "2022 scheme year"),
            new MarketingYearCode("2021", "2021 scheme year"),
            new MarketingYearCode("2020", "2020 scheme year"),
            new MarketingYearCode("1997", "1997 scheme year")
        );

        _context.SaveChanges();
    }

    [Fact]
    public async Task GetShouldReturnValidYearCodes()
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

        var route = _context.InvoiceRoutes.Include(r => r.MarketingYearCodes)
            .Single(r => r.InvoiceType.Code == "AP"
                        && r.Organisation.Code == "EA"
                        && r.SchemeType.Code == "CS"
                        && r.PaymentType.Code == "EU");

        route.MarketingYearCodes.Add(_context.MarketingYearCodes.Single(x => x.Code == "2022"));
        route.MarketingYearCodes.Add(_context.MarketingYearCodes.Single(x => x.Code == "2021"));
        route.MarketingYearCodes.Add(_context.MarketingYearCodes.Single(x => x.Code == "2020"));

        await _context.SaveChangesAsync();

        // Act
        var codes = (await _yearDataService.Get(route)).ToArray();

        // Assert
        Assert.NotNull(codes);
        Assert.Equal(3, codes.Length);
        Assert.Equal("2022", codes[0].Code);
        Assert.Equal("2022 scheme year", codes[0].Description);

        Assert.Equal("2021", codes[1].Code);
        Assert.Equal("2021 scheme year", codes[1].Description);

        Assert.Equal("2020", codes[2].Code);
        Assert.Equal("2020 scheme year", codes[2].Description);
    }

    [Fact]
    public async Task AddShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _yearDataService.Add(null!, null!));
    }

    [Fact]
    public async Task AddRangeShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _yearDataService.AddRange(null!, null!));
    }

    [Fact]
    public async Task UpdateShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _yearDataService.Update(null!, null!));
    }
}