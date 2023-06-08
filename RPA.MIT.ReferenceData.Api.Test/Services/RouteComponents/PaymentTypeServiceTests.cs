using RPA.MIT.ReferenceData.Api.Exceptions;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Api.Services.RouteComponents;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Test.Services.RouteComponents;

public class PaymentTypeServiceTests
{
    private readonly IPaymentTypeDataService _paymentTypeDataService;

    public PaymentTypeServiceTests()
    {
        var context = Utilites.GetRefDataContext();

        _paymentTypeDataService = new PaymentTypeDataService(context);

        context.InvoiceTypes.Add(new InvoiceType("AP", "Accounts Payable"));
        context.InvoiceTypes.Add(new InvoiceType("AR", "Accounts Receivable"));
        context.Organisations.Add(new Organisation("EA", "Environment Agency"));
        context.Organisations.Add(new Organisation("RPA", "Rural Payments Agency"));
        context.PaymentTypes.Add(new PaymentType("EU", "European Union"));
        context.PaymentTypes.Add(new PaymentType("DOM", "Domestic"));

        context.SchemeTypes.AddRange(
            new SchemeType("LS", "Legacy"),
            new SchemeType("CS", "Current"),
            new SchemeType("DOM", "Domestic")
        );

        context.SaveChanges();

        var mockRoutes = new[]
        {
            Utilites.CreateInvoiceRoute(context, "AP", "EA", "CS", "DOM"),
            Utilites.CreateInvoiceRoute(context, "AP", "EA", "DOM", "DOM"),
            Utilites.CreateInvoiceRoute(context, "AP", "RPA", "DOM", "DOM"),
            Utilites.CreateInvoiceRoute(context, "AR", "EA", "LS", "EU"),
            Utilites.CreateInvoiceRoute(context, "AR", "RPA", "DOM", "EU"),
            Utilites.CreateInvoiceRoute(context, "AR", "RPA", "CS", "DOM")
        };

        context.InvoiceRoutes.AddRange(mockRoutes);

        context.SaveChanges();
    }

    [Fact]
    public async Task GetShouldReturnAllPaymentTypes()
    {
        // Act
        var types = (await _paymentTypeDataService.Get()).ToArray();

        // Assert
        Assert.NotNull(types);
        Assert.Equal(2, types.Length);
        Assert.Contains(types, type => "EU" == type.Code && type.Description == "European Union");
        Assert.Contains(types, type => "DOM" == type.Code && type.Description == "Domestic");
    }

    [Fact]
    public async Task GetFilterShouldReturnSchemeTypesWithInvoiceType()
    {
        // Act
        var types = (await _paymentTypeDataService.Get("AP")).ToArray();

        // Assert
        Assert.NotNull(types);
        Assert.Single(types);
        Assert.Contains(types, type => "DOM" == type.Code && type.Description == "Domestic");
    }

    [Fact]
    public async Task GetShouldReturnPaymentTypesWithDeliveryBody()
    {
        // Act
        var types = (await _paymentTypeDataService.Get(null, "EA")).ToArray();

        // Assert
        Assert.NotNull(types);
        Assert.Equal(2, types.Length);
        Assert.Contains(types, type => "EU" == type.Code && type.Description == "European Union");
        Assert.Contains(types, type => "DOM" == type.Code && type.Description == "Domestic");
    }

    [Fact]
    public async Task GetShouldReturnPaymentTypesWithInvoiceTypeAndDeliveryBody()
    {
        // Act
        var types = (await _paymentTypeDataService.Get("AP", "RPA")).ToArray();

        // Assert
        Assert.NotNull(types);
        Assert.Single(types);
        Assert.Contains(types, type => "DOM" == type.Code && type.Description == "Domestic");
    }

    [Fact]
    public async Task GetShouldReturnPaymentTypesWithAllParams()
    {
        // Act
        var types = (await _paymentTypeDataService.Get("AR", "RPA", "CS")).ToArray();

        // Assert
        Assert.NotNull(types);
        Assert.Single(types);
        Assert.Contains(types, type => "DOM" == type.Code && type.Description == "Domestic");
    }

    [Fact]
    public async Task InvoiceTypeNotFoundShouldThrowException()
    {
        // Act / Assert
        await Assert.ThrowsAsync<InvoiceTypeNotFoundException>(() => _paymentTypeDataService.Get("ZZ"));
    }

    [Fact]
    public async Task DeliveryBodyNotFoundShouldThrowException()
    {
        // Act / Assert
        await Assert.ThrowsAsync<DeliveryBodyNotFoundException>(() => _paymentTypeDataService.Get("AP", "ZZ"));
    }

    [Fact]
    public async Task SchemeTypeNotFoundShouldThrowException()
    {
        // Act / Assert
        await Assert.ThrowsAsync<SchemeTypeNotFoundException>(() => _paymentTypeDataService.Get("AP", "EA", "ZZ"));
    }
}
