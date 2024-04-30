using Microsoft.AspNetCore.Http;
using Moq;
using RPA.MIT.ReferenceData.Api.Endpoints.RouteComponents;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Test.Endpoints.RouteComponents;

public class InvoiceTypeEndpointsTests
{
    private readonly Mock<IInvoiceTypeDataService> _mockDataService = new();

    [Fact]
    public async Task GetShouldReturn200OkIfSchemeTypesFound()
    {
        // Arrange
        var mockTypes = new InvoiceType[]
        {
            new("AP", "Accounts Payable"),
            new("AR", "Accounts Receivable")
        };

        _mockDataService.Setup(x => x.Get()).ReturnsAsync(mockTypes);

        // Act
        var result = await InvoiceTypeEndpoints.GetInvoiceTypes(_mockDataService.Object);

        var (statusCode, body) = await Utilites.GetResponse<List<InvoiceType>>(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status200OK, statusCode);
        Assert.NotEmpty(body);

        Assert.Contains(body, type => type.Code == "AP" && type.Description == "Accounts Payable");
        Assert.Contains(body, type => type.Code == "AR" && type.Description == "Accounts Receivable");
    }

    [Fact]
    public async Task GetShouldReturn404NotFoundIfNoSchemeTypesFound()
    {
        // Arrange
        var mockTypes = Array.Empty<InvoiceType>();

        _mockDataService.Setup(x => x.Get()).ReturnsAsync(mockTypes);

        // Act
        var result = await InvoiceTypeEndpoints.GetInvoiceTypes(_mockDataService.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, statusCode);
    }
}
