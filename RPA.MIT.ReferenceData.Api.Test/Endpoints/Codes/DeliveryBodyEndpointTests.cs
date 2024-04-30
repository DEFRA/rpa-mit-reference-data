using Microsoft.AspNetCore.Http;
using Moq;
using RPA.MIT.ReferenceData.Api.Endpoints.Codes;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;

namespace RPA.MIT.ReferenceData.Api.Test.Endpoints.Codes;

public class DeliveryBodyEndpointTests
{
    private readonly Mock<IInvoiceRouteService> _mockRouteService = new();
    private readonly Mock<IReferenceDataService<DeliveryBodyCode>> _mockDataService = new();

    [Fact]
    public async Task GetShouldReturn200OkIfDeliveryBodyCodesFound()
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

        var mockDbCodes = new DeliveryBodyCode[]
        {
            new("RP01", "Test description 1"),
            new("RP02", "Test description 2"),
            new("RP04", "Test description 4")
        };

        _mockDataService.Setup(x => x.Get(mockRoute)).ReturnsAsync(mockDbCodes);

        // Act
        var result = await DeliveryBodyEndpoints.GetDeliveryBodyCodes("AP", "EA", "CS", "DOM", _mockRouteService.Object, _mockDataService.Object);

        var (statusCode, body) = await Utilites.GetResponse<List<FundCode>>(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status200OK, statusCode);
        Assert.NotEmpty(body);
        Assert.Equal("RP01", body[0].Code);
        Assert.Equal("Test description 1", body[0].Description);

        Assert.Equal("RP02", body[1].Code);
        Assert.Equal("Test description 2", body[1].Description);

        Assert.Equal("RP04", body[2].Code);
        Assert.Equal("Test description 4", body[2].Description);
    }

    [Theory]
    [InlineData("AP", "EA", "LS", null)]
    [InlineData("AP", "EA", null, null)]
    [InlineData("AP", null, null, null)]
    [InlineData(null, null, null, null)]
    public async Task GetWithMissingParametersShouldReturn400BadRequest(string? invoiceType, string? deliveryBody, string? schemeType, string? paymentType)
    {
        // Act
        var result = await DeliveryBodyEndpoints.GetDeliveryBodyCodes(invoiceType, deliveryBody, schemeType, paymentType, _mockRouteService.Object, _mockDataService.Object);

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
        var result = await DeliveryBodyEndpoints.GetDeliveryBodyCodes("NA", "NA", "NA", "NA", _mockRouteService.Object, _mockDataService.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, statusCode);
    }

    [Fact]
    public async Task GetShouldReturn204NoContentIfNoDeliveryBodyCodes()
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

        var mockDbCodes = Array.Empty<DeliveryBodyCode>();

        _mockDataService.Setup(x => x.Get(mockRoute)).ReturnsAsync(mockDbCodes);

        // Act
        var result = await DeliveryBodyEndpoints.GetDeliveryBodyCodes("AP", "EA", "CS", "DOM", _mockRouteService.Object, _mockDataService.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, statusCode);
    }
}
