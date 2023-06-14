using RPA.MIT.ReferenceData.Api.Exceptions;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Api.Services.RouteComponents;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Test.Services.RouteComponents;

public class OrganisationServiceTests
{
    private readonly IOrganisationDataService _orgDataService;

    public OrganisationServiceTests()
    {
        var context = Utilites.GetRefDataContext();

        _orgDataService = new OrganisationDataService(context);

        context.InvoiceTypes.Add(new InvoiceType("AP", "Accounts Payable"));
        context.InvoiceTypes.Add(new InvoiceType("AR", "Accounts Receivable"));

        context.Organisations.Add(new Organisation("EA", "Environment Agency"));
        context.Organisations.Add(new Organisation("RPA", "Rural Payments Agency"));
        context.Organisations.Add(new Organisation("NE", "Natural England"));

        context.SchemeTypes.Add(new SchemeType("DOM", "Domestic"));
        context.SchemeTypes.Add(new SchemeType("CS", "Current"));

        context.PaymentTypes.Add(new PaymentType("EU", "EU"));

        context.SaveChanges();

        var mockRoutes = new[]
        {
            Utilites.CreateInvoiceRoute(context, "AP", "EA", "CS", "EU"),
            Utilites.CreateInvoiceRoute(context, "AP", "EA", "DOM", "EU"),
            Utilites.CreateInvoiceRoute(context, "AP", "RPA", "DOM", "EU"),
            Utilites.CreateInvoiceRoute(context, "AR", "EA", "CS", "EU"),
            Utilites.CreateInvoiceRoute(context, "AR", "RPA", "DOM", "EU"),
            Utilites.CreateInvoiceRoute(context, "AR", "NE", "DOM", "EU")
        };

        context.InvoiceRoutes.AddRange(mockRoutes);

        context.SaveChanges();
    }

    [Fact]
    public async Task GetShouldReturnAllOrganisations()
    {
        // Act
        var types = (await _orgDataService.Get()).ToArray();

        // Assert
        Assert.NotNull(types);
        Assert.Equal(3, types.Length);
        Assert.Contains(types, type => "EA" == type.Code && type.Description == "Environment Agency");
        Assert.Contains(types, type => "RPA" == type.Code && type.Description == "Rural Payments Agency");
        Assert.Contains(types, type => "NE" == type.Code && type.Description == "Natural England");
    }

    [Fact]
    public async Task GetFilterShouldReturnSchemeTypesWithInvoiceType()
    {
        // Act
        var types = (await _orgDataService.Get("AP")).ToArray();

        // Assert
        Assert.NotNull(types);
        Assert.Equal(2, types.Length);
        Assert.Contains(types, type => "EA" == type.Code && type.Description == "Environment Agency");
        Assert.Contains(types, type => "RPA" == type.Code && type.Description == "Rural Payments Agency");
    }

    [Fact]
    public async Task InvoiceTypeNotFoundShouldThrowException()
    {
        // Act / Assert
        await Assert.ThrowsAsync<InvoiceTypeNotFoundException>(() => _orgDataService.Get("ZZ"));
    }
}
