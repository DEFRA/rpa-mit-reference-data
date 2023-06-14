using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Services.Codes;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Test.Services.Codes;

public class CombinationDataServiceTests
{
    private readonly CombinationDataService _combinationDataService;
    private readonly ReferenceDataContext _context;

    public CombinationDataServiceTests()
    {
        _context = Utilites.GetRefDataContext();

        _combinationDataService = new CombinationDataService(_context);

        _context.InvoiceTypes.Add(new InvoiceType("AP", "Accounts Payable"));
        _context.Organisations.Add(new Organisation("EA", "Environment Agency"));
        _context.SchemeTypes.Add(new SchemeType("CS", "CS"));
        _context.PaymentTypes.Add(new PaymentType("EU", "European Union"));

        _context.AccountCodes.Add(new AccountCode("SOS130", "Test account 1"));

        _context.SchemeCodes.AddRange(
            new SchemeCode("12345", "Test scheme 1"),
            new SchemeCode("12346", "Test scheme 2"),
            new SchemeCode("12347", "Test scheme 3")
        );

        _context.DeliveryBodyCodes.AddRange(
            new DeliveryBodyCode("TD00", "Test delivery body 1"),
            new DeliveryBodyCode("TD01", "Test delivery body 2")
        );

        _context.SaveChanges();

        var mockRoute = new InvoiceRoute(
            _context.InvoiceTypes.Single(x => x.Code == "AP"),
            _context.Organisations.Single(x => x.Code == "EA"),
            _context.SchemeTypes.Single(x => x.Code == "CS"),
            _context.PaymentTypes.Single(x => x.Code == "EU")
        );

        _context.InvoiceRoutes.Add(mockRoute);

        _context.SaveChanges();
    }

    [Fact]
    public async Task GetShouldReturnValidCombinations()
    {
        // Arrange
        var route = _context.InvoiceRoutes.Include(r => r.Combinations)
            .Single(r => r.InvoiceType.Code == "AP"
                        && r.Organisation.Code == "EA"
                        && r.SchemeType.Code == "CS"
                        && r.PaymentType.Code == "EU");

        route.Combinations.Add(new Combination(
                route,
                _context.AccountCodes.Single(x => x.Code == "SOS130"),
                _context.SchemeCodes.Single(x => x.Code == "12345"),
                _context.DeliveryBodyCodes.Single(x => x.Code == "TD00")
            )
        );

        route.Combinations.Add(new Combination(
                route,
                _context.AccountCodes.Single(x => x.Code == "SOS130"),
                _context.SchemeCodes.Single(x => x.Code == "12346"),
                _context.DeliveryBodyCodes.Single(x => x.Code == "TD01")
            )
        );

        route.Combinations.Add(new Combination(
                route,
                _context.AccountCodes.Single(x => x.Code == "SOS130"),
                _context.SchemeCodes.Single(x => x.Code == "12347"),
                _context.DeliveryBodyCodes.Single(x => x.Code == "TD01")
            )
        );

        await _context.SaveChangesAsync();

        // Act
        var combinations = (await _combinationDataService.Get(route)).ToArray();

        // Assert
        Assert.NotNull(combinations);
        Assert.Equal(3, combinations.Length);
        Assert.Equal("SOS130", combinations[0].AccountCode);
        Assert.Equal("12345", combinations[0].SchemeCode);
        Assert.Equal("TD00", combinations[0].DeliveryBodyCode);

        Assert.Equal("SOS130", combinations[1].AccountCode);
        Assert.Equal("12346", combinations[1].SchemeCode);
        Assert.Equal("TD01", combinations[1].DeliveryBodyCode);

        Assert.Equal("SOS130", combinations[2].AccountCode);
        Assert.Equal("12347", combinations[2].SchemeCode);
        Assert.Equal("TD01", combinations[2].DeliveryBodyCode);
    }

    [Fact]
    public async Task AddShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _combinationDataService.Add(null!, null!));
    }

    [Fact]
    public async Task AddRangeShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _combinationDataService.AddRange(null!, null!));
    }

    [Fact]
    public async Task UpdateShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _combinationDataService.Update(null!, null!));
    }
}