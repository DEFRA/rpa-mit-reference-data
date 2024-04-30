using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Api.Services.Codes;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Test.Services.Codes;

public class SchemeDataServiceTests
{
    private readonly IReferenceDataService<SchemeCode> _schemeDataService;
    private readonly ReferenceDataContext _context;

    public IReferenceDataService<SchemeCode> SchemeDataService => _schemeDataService;

    public SchemeDataServiceTests()
    {
        _context = Utilites.GetRefDataContext();

        _schemeDataService = new SchemeDataService(_context);

        _context.InvoiceTypes.Add(new InvoiceType("AP", "Accounts Payable"));
        _context.Organisations.Add(new Organisation("EA", "Environment Agency"));
        _context.SchemeTypes.Add(new SchemeType("CS", "CS"));
        _context.PaymentTypes.Add(new PaymentType("EU", "European Union"));

        _context.SchemeCodes.AddRange(
            new SchemeCode("12345", "Test description 1"),
            new SchemeCode("12346", "Test description 2"),
            new SchemeCode("12347", "Test description 3"),
            new SchemeCode("12348", "Test description 4")
        );

        _context.SaveChanges();
    }

    [Fact]
    public async Task GetShouldReturnValidSchemeCodes()
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

        var route = _context.InvoiceRoutes.Include(r => r.SchemeCodes)
            .Single(r => r.InvoiceType.Code == "AP"
                        && r.Organisation.Code == "EA"
                        && r.SchemeType.Code == "CS"
                        && r.PaymentType.Code == "EU");

        route.SchemeCodes.Add(_context.SchemeCodes.Single(x => x.Code == "12345"));
        route.SchemeCodes.Add(_context.SchemeCodes.Single(x => x.Code == "12346"));
        route.SchemeCodes.Add(_context.SchemeCodes.Single(x => x.Code == "12348"));

        await _context.SaveChangesAsync();

        // Act
        var codes = (await SchemeDataService.Get(route)).ToArray();

        // Assert
        Assert.NotNull(codes);
        Assert.Equal(3, codes.Length);
        Assert.Equal("12345", codes[0].Code);
        Assert.Equal("Test description 1", codes[0].Description);

        Assert.Equal("12346", codes[1].Code);
        Assert.Equal("Test description 2", codes[1].Description);

        Assert.Equal("12348", codes[2].Code);
        Assert.Equal("Test description 4", codes[2].Description);
    }

    [Fact]
    public async Task AddShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => SchemeDataService.Add(null!, null!));
    }

    [Fact]
    public async Task AddRangeShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => SchemeDataService.AddRange(null!, null!));
    }

    [Fact]
    public async Task UpdateShouldThrowNotImplemented()
    {
        // Act / Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => SchemeDataService.Update(null!, null!));
    }
}
