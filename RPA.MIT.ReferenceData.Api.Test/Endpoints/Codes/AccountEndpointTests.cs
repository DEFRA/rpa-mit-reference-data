using Microsoft.AspNetCore.Http;
using Moq;
using RPA.MIT.ReferenceData.Api.Endpoints.Codes;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;

namespace RPA.MIT.ReferenceData.Api.Test.Endpoints.Codes;

public class AccountEndpointTests
{
    private readonly Mock<IInvoiceRouteService> _mockRouteService = new();
    private readonly Mock<IReferenceDataService<AccountCode>> _mockDataService = new();

    [Fact]
    public async Task GetShouldReturn200OkIfAccountsFound()
    {
        // Arrange
        var mockRoute = Utilites.CreateInvoiceRoute("AP", "EA", "CS", "DOM");

        _mockRouteService.Setup(
            x => x.GetRoute(
                It.Is<string>(it => it == "AP"),
                It.Is<string>(db => db == "EA"),
                It.Is<string>(st => st == "CS"),
                It.Is<string>(py => py == "DOM")
            )
        ).ReturnsAsync(mockRoute);

        var mockAccounts = new AccountCode[]
        {
            new("SOS123", "Test description 1"),
            new("SOS124", "Test description 2"),
            new("SOS125", "Test description 3")
        };

        _mockDataService.Setup(x => x.Get(mockRoute)).ReturnsAsync(mockAccounts);

        // Act
        var result = await AccountEndpoints.GetAccounts("AP", "EA", "CS", "DOM", _mockRouteService.Object, _mockDataService.Object);

        var (statusCode, body) = await Utilites.GetResponse<List<AccountCode>>(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status200OK, statusCode);
        Assert.NotEmpty(body);
        Assert.Equal("SOS123", body[0].Code);
        Assert.Equal("Test description 1", body[0].Description);
        Assert.Equal("SOS124", body[1].Code);
        Assert.Equal("Test description 2", body[1].Description);
        Assert.Equal("SOS125", body[2].Code);
        Assert.Equal("Test description 3", body[2].Description);
    }

    [Theory]
    [InlineData("AP", "EA", "LS", null)]
    [InlineData("AP", "EA", null, null)]
    [InlineData("AP", null, null, null)]
    [InlineData(null, null, null, null)]
    public async Task GetWithMissingParametersShouldReturn400BadRequest(string? invoiceType, string? deliveryBody, string? schemeType, string? paymentType)
    {
        // Act
        var result = await AccountEndpoints.GetAccounts(invoiceType, deliveryBody, schemeType, paymentType, _mockRouteService.Object, _mockDataService.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCode);
    }

    [Fact]
    public async Task GetShouldReturn404NotFoundIfRouteNull()
    {
        // Arrange
        InvoiceRoute mockRoute = null!;

        _mockRouteService.Setup(
            x => x.GetRoute(
                It.Is<string>(it => it == "NA"),
                It.Is<string>(db => db == "NA"),
                It.Is<string>(st => st == "NA"),
                It.Is<string>(py => py == "NA")
            )
        ).ReturnsAsync(mockRoute);

        // Act
        var result = await AccountEndpoints.GetAccounts("NA", "NA", "NA", "NA", _mockRouteService.Object, _mockDataService.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, statusCode);
    }

    [Fact]
    public async Task GetShouldReturn204NoContentIfNoAccounts()
    {
        // Arrange
        var mockRoute = Utilites.CreateInvoiceRoute("AP", "EA", "CS", "DOM");

        _mockRouteService.Setup(
            x => x.GetRoute(
                It.Is<string>(it => it == "AP"),
                It.Is<string>(db => db == "EA"),
                It.Is<string>(st => st == "CS"),
                It.Is<string>(py => py == "DOM")
            )
        ).ReturnsAsync(mockRoute);

        var mockAccounts = Array.Empty<AccountCode>();

        _mockDataService.Setup(x => x.Get(mockRoute)).ReturnsAsync(mockAccounts);

        // Act
        var result = await AccountEndpoints.GetAccounts("AP", "EA", "CS", "DOM", _mockRouteService.Object, _mockDataService.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, statusCode);
    }
}
