using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Api.Services.Codes;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Test.Services.Codes;

public class DeliveryBodyCodeDataServiceTests
{
    private readonly IReferenceDataService<DeliveryBodyCode> _dbCodeDataService;
    private readonly ReferenceDataContext _context;

    public DeliveryBodyCodeDataServiceTests()
    {
        _context = Utilites.GetRefDataContext();

        _dbCodeDataService = new DeliveryBodyCodeDataService(_context);

        _context.InvoiceTypes.Add(new InvoiceType("AP", "Accounts Payable"));
        _context.Organisations.Add(new Organisation("EA", "Environment Agency"));
        _context.SchemeTypes.Add(new SchemeType("CS", "CS"));
        _context.PaymentTypes.Add(new PaymentType("EU", "European Union"));

        _context.DeliveryBodyCodes.AddRange(
            new DeliveryBodyCode("RP01", "Test description 1"),
            new DeliveryBodyCode("RP02", "Test description 2"),
            new DeliveryBodyCode("RP03", "Test description 3"),
            new DeliveryBodyCode("RP04", "Test description 4")
        );

        _context.SaveChanges();
    }

    [Fact]
    public async Task GetShouldReturnValidDeliveryBodyCode()
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

        var route = _context.InvoiceRoutes.Include(r => r.DeliveryBodyCodes)
            .Single(r => r.InvoiceType.Code == "AP"
                        && r.Organisation.Code == "EA"
                        && r.SchemeType.Code == "CS"
                        && r.PaymentType.Code == "EU");

        route.DeliveryBodyCodes.Add(_context.DeliveryBodyCodes.Single(x => x.Code == "RP01"));
        route.DeliveryBodyCodes.Add(_context.DeliveryBodyCodes.Single(x => x.Code == "RP02"));
        route.DeliveryBodyCodes.Add(_context.DeliveryBodyCodes.Single(x => x.Code == "RP03"));

        await _context.SaveChangesAsync();

        // Act
        var codes = (await _dbCodeDataService.Get(route)).ToArray();

        // Assert
        Assert.NotNull(codes);
        Assert.Equal(3, codes.Length);
        Assert.Equal("RP01", codes[0].Code);
        Assert.Equal("Test description 1", codes[0].Description);

        Assert.Equal("RP02", codes[1].Code);
        Assert.Equal("Test description 2", codes[1].Description);

        Assert.Equal("RP03", codes[2].Code);
        Assert.Equal("Test description 3", codes[2].Description);
    }

    [Fact]
    public async Task AddShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _dbCodeDataService.Add(null!, null!));
    }

    [Fact]
    public async Task AddRangeShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _dbCodeDataService.AddRange(null!, null!));
    }

    [Fact]
    public async Task UpdateShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _dbCodeDataService.Update(null!, null!));
    }
}