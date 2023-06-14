using Microsoft.AspNetCore.Http;
using Moq;
using RPA.MIT.ReferenceData.Api.Endpoints.Codes;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;

namespace RPA.MIT.ReferenceData.Api.Test.Endpoints.Codes;

public class FundEndpointTests
{
    private readonly Mock<IInvoiceRouteService> _mockRouteService = new();
    private readonly Mock<IReferenceDataService<FundCode>> _mockDataService = new();

    [Fact]
    public async Task GetShouldReturn200OkIfFundsFound()
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

        var mockFunds = new FundCode[]
        {
            new("DRD00"),
            new("EXQ00"),
            new("DOM10")
        };

        _mockDataService.Setup(x => x.Get(mockRoute)).ReturnsAsync(mockFunds);

        // Act
        var result = await FundEndpoints.GetFunds("AP", "EA", "CS", "DOM", _mockRouteService.Object, _mockDataService.Object);

        var (statusCode, body) = await Utilites.GetResponse<List<FundCode>>(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status200OK, statusCode);
        Assert.NotEmpty(body);
        Assert.Equal("DRD00", body[0].Code);
        Assert.Empty(body[0].Description);

        Assert.Equal("EXQ00", body[1].Code);
        Assert.Empty(body[1].Description);

        Assert.Equal("DOM10", body[2].Code);
        Assert.Empty(body[2].Description);
    }

    [Theory]
    [InlineData("AP", "EA", "LS", null)]
    [InlineData("AP", "EA", null, null)]
    [InlineData("AP", null, null, null)]
    [InlineData(null, null, null, null)]
    public async Task GetWithMissingParametersShouldReturn400BadRequest(string? invoiceType, string? deliveryBody, string? schemeType, string? paymentType)
    {
        // Act
        var result = await FundEndpoints.GetFunds(invoiceType, deliveryBody, schemeType, paymentType, _mockRouteService.Object, _mockDataService.Object);

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
        var result = await FundEndpoints.GetFunds("NA", "NA", "NA", "NA", _mockRouteService.Object, _mockDataService.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, statusCode);
    }

    [Fact]
    public async Task GetShouldReturn204NoContentIfNoFunds()
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

        var mockFunds = Array.Empty<FundCode>();

        _mockDataService.Setup(x => x.Get(mockRoute)).ReturnsAsync(mockFunds);

        // Act
        var result = await FundEndpoints.GetFunds("AP", "EA", "CS", "DOM", _mockRouteService.Object, _mockDataService.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, statusCode);
    }
}
