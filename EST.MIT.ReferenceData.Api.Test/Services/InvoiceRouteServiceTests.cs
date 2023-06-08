using EST.MIT.ReferenceData.Api.Services;
using EST.MIT.ReferenceData.Data;
using EST.MIT.ReferenceData.Data.Models;
using EST.MIT.ReferenceData.Data.Models.RouteComponents;

namespace EST.MIT.ReferenceData.Api.Test.Services;

public class InvoiceRouteServiceTests
{
    private readonly InvoiceRouteService _routeService;
    private readonly ReferenceDataContext _context;

    public InvoiceRouteServiceTests()
    {
        _context = Utilites.GetRefDataContext();

        _routeService = new InvoiceRouteService(_context);

        _context.InvoiceTypes.Add(new InvoiceType("AP", "Accounts Payable"));
        _context.Organisations.Add(new Organisation("EA", "Environment Agency"));
        _context.SchemeTypes.Add(new SchemeType("CS", "CS"));
        _context.PaymentTypes.Add(new PaymentType("EU", "European Union"));

        _context.SaveChanges();
    }

    [Fact]
    public async Task GetRouteShouldReturnRouteIfFound()
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

        // Act
        var route = await _routeService.GetRoute("AP", "EA", "CS", "EU");

        // Assert
        Assert.NotNull(route);
        Assert.IsType<InvoiceRoute>(route);
    }

    [Fact]
    public async Task GetRouteShouldReturnNullIfRouteNotFound()
    {
        // Act
        var route = await _routeService.GetRoute("NA", "NA", "NA", "NA");

        // Assert
        Assert.Null(route);
    }
}