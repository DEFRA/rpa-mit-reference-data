using EST.MIT.ReferenceData.Api.Exceptions;
using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Api.Services.RouteComponents;
using EST.MIT.ReferenceData.Data.Models.RouteComponents;

namespace EST.MIT.ReferenceData.Api.Test.Services.RouteComponents;

public class SchemeTypeServiceTests
{
    private readonly ISchemeTypeDataService _schemeDataService;

    public SchemeTypeServiceTests()
    {
        var context = Utilites.GetRefDataContext();

        _schemeDataService = new SchemeTypeDataService(context);

        context.InvoiceTypes.Add(new InvoiceType("AP", "Accounts Payable"));
        context.InvoiceTypes.Add(new InvoiceType("AR", "Accounts Receivable"));
        context.Organisations.Add(new Organisation("EA", "Environment Agency"));
        context.Organisations.Add(new Organisation("RPA", "Rural Payments Agency"));
        context.PaymentTypes.Add(new PaymentType("EU", "European Union"));

        context.SchemeTypes.AddRange(
            new SchemeType("LS", "Legacy"),
            new SchemeType("CS", "Current"),
            new SchemeType("DOM", "Domestic"),
            new SchemeType("R", "Random type")
        );

        context.SaveChanges();

        var mockRoutes = new[]
        {
            Utilites.CreateInvoiceRoute(context, "AP", "EA", "CS", "EU"),
            Utilites.CreateInvoiceRoute(context, "AP", "EA", "LS", "EU"),
            Utilites.CreateInvoiceRoute(context, "AP", "EA", "DOM", "EU"),
            Utilites.CreateInvoiceRoute(context, "AP", "RPA", "DOM", "EU"),
            Utilites.CreateInvoiceRoute(context, "AR", "RPA", "R", "EU")
        };

        context.InvoiceRoutes.AddRange(mockRoutes);

        context.SaveChanges();
    }

    [Fact]
    public async Task GetShouldReturnAllSchemeTypes()
    {
        // Act
        var types = (await _schemeDataService.Get()).ToArray();

        // Assert
        Assert.NotNull(types);
        Assert.Equal(4, types.Length);
        Assert.Equal("LS", types[0].Code);
        Assert.Equal("Legacy", types[0].Description);

        Assert.Equal("CS", types[1].Code);
        Assert.Equal("Current", types[1].Description);

        Assert.Equal("DOM", types[2].Code);
        Assert.Equal("Domestic", types[2].Description);

        Assert.Equal("R", types[3].Code);
        Assert.Equal("Random type", types[3].Description);
    }

    [Fact]
    public async Task GetFilterShouldReturnSchemeTypesWithInvoiceType()
    {
        // Act
        var types = (await _schemeDataService.Get("AP")).ToArray();

        // Assert
        Assert.NotNull(types);
        Assert.Equal(3, types.Length);
        Assert.Contains(types, type => "LS" == type.Code && type.Description == "Legacy");
        Assert.Contains(types, type => "CS" == type.Code && type.Description == "Current");
        Assert.Contains(types, type => "DOM" == type.Code && type.Description == "Domestic");
    }

    [Fact]
    public async Task GetShouldReturnSchemeTypesWithDeliveryBody()
    {
        // Act
        var types = (await _schemeDataService.Get(null, "RPA")).ToArray();

        // Assert
        Assert.NotNull(types);
        Assert.Equal(2, types.Length);
        Assert.Contains(types, type => "DOM" == type.Code && type.Description == "Domestic");
        Assert.Contains(types, type => "R" == type.Code && type.Description == "Random type");
    }

    [Fact]
    public async Task GetShouldReturnSchemeTypesWithInvoiceTypeAndDeliveryBody()
    {
        // Act
        var types = (await _schemeDataService.Get("AR", "RPA")).ToArray();

        // Assert
        Assert.NotNull(types);
        Assert.Single(types);
        Assert.Contains(types, type => "R" == type.Code && type.Description == "Random type");
    }

    [Fact]
    public async Task InvoiceTypeNotFoundShouldThrowException()
    {
        // Act / Assert
        await Assert.ThrowsAsync<InvoiceTypeNotFoundException>(() => _schemeDataService.Get("ZZ"));
    }

    [Fact]
    public async Task DeliveryBodyNotFoundShouldThrowException()
    {
        // Act / Assert
        await Assert.ThrowsAsync<DeliveryBodyNotFoundException>(() => _schemeDataService.Get("AP", "ZZ"));
    }
}
