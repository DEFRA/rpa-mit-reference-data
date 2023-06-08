using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Api.Services.RouteComponents;
using EST.MIT.ReferenceData.Data.Models.RouteComponents;

namespace EST.MIT.ReferenceData.Api.Test.Services.RouteComponents;

public class InvoiceTypeServiceTests
{
    private readonly IInvoiceTypeDataService _invoiceTypeDataService;

    public InvoiceTypeServiceTests()
    {
        var context = Utilites.GetRefDataContext();

        _invoiceTypeDataService = new InvoiceTypeDataService(context);

        context.InvoiceTypes.Add(new InvoiceType("AP", "Accounts Payable"));
        context.InvoiceTypes.Add(new InvoiceType("AR", "Accounts Receivable"));

        context.SaveChanges();

    }

    [Fact]
    public async Task GetShouldReturnAllInvoiceTypes()
    {
        // Act
        var types = (await _invoiceTypeDataService.Get()).ToArray();

        // Assert
        Assert.NotNull(types);
        Assert.Equal(2, types.Length);

        Assert.Contains(types, type => type.Code == "AP" && type.Description == "Accounts Payable");
        Assert.Contains(types, type => type.Code == "AR" && type.Description == "Accounts Receivable");
    }
}
