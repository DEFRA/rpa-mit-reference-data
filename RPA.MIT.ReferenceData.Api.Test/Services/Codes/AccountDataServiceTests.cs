using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Services.Codes;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Test.Services.Codes;

public class AccountDataServiceTests
{
    private readonly AccountDataService _accountDataService;
    private readonly ReferenceDataContext _context;

    public AccountDataServiceTests()
    {
        _context = Utilites.GetRefDataContext();

        _accountDataService = new AccountDataService(_context);

        _context.InvoiceTypes.Add(new InvoiceType("AP", "Accounts Payable"));
        _context.Organisations.Add(new Organisation("EA", "Environment Agency"));
        _context.SchemeTypes.Add(new SchemeType("CS", "CS"));
        _context.PaymentTypes.Add(new PaymentType("EU", "European Union"));

        _context.SaveChanges();
    }

    [Fact]
    public async Task GetShouldReturnAccountCodes()
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

        var route = _context.InvoiceRoutes.Include(r => r.AccountCodes)
            .Single(r => r.InvoiceType.Code == "AP"
                         && r.Organisation.Code == "EA"
                         && r.SchemeType.Code == "CS"
                         && r.PaymentType.Code == "EU");

        route.AccountCodes.Add(new AccountCode("SOS130", "Test description 1"));
        route.AccountCodes.Add(new AccountCode("SOS131", "Test description 2"));
        route.AccountCodes.Add(new AccountCode("SOS132", "Test description 3"));

        await _context.SaveChangesAsync();

        // Act
        var accounts = (await _accountDataService.Get(mockRoute)).ToArray();

        // Assert
        Assert.NotNull(accounts);
        Assert.Equal(3, accounts.Length);
        Assert.Equal("SOS130", accounts[0].Code);
        Assert.Equal("Test description 1", accounts[0].Description);
        Assert.Equal("SOS131", accounts[1].Code);
        Assert.Equal("Test description 2", accounts[1].Description);
        Assert.Equal("SOS132", accounts[2].Code);
        Assert.Equal("Test description 3", accounts[2].Description);
    }

    [Fact]
    public async Task AddShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _accountDataService.Add(null!, null!));
    }

    [Fact]
    public async Task AddRangeShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _accountDataService.AddRange(null!, null!));
    }

    [Fact]
    public async Task UpdateShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _accountDataService.Update(null!, null!));
    }
}