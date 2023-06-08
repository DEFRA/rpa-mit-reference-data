using EST.MIT.ReferenceData.Api.Endpoints.Codes;
using EST.MIT.ReferenceData.Api.Interfaces;
using EST.MIT.ReferenceData.Data.Models;
using EST.MIT.ReferenceData.Data.Models.Codes;
using Microsoft.AspNetCore.Http;
using Moq;

namespace EST.MIT.ReferenceData.Api.Test.Endpoints.Codes;

public class MarketingYearEndpointTests
{
    private readonly Mock<IInvoiceRouteService> _mockRouteService = new();
    private readonly Mock<IReferenceDataService<MarketingYearCode>> _mockDataService = new();

    [Fact]
    public async Task GetShouldReturn200OkIfYearCodesFound()
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

        var mockYearCodes = new MarketingYearCode[]
        {
            new("2022", "2022 scheme year"),
            new("2021", "2021 scheme year"),
            new("2020", "2020 scheme year")
        };

        _mockDataService.Setup(x => x.Get(mockRoute)).ReturnsAsync(mockYearCodes);

        // Act
        var result = await MarketingYearEndpoints.GetYears("AP", "EA", "CS", "DOM", _mockRouteService.Object, _mockDataService.Object);

        var (statusCode, body) = await Utilites.GetResponse<List<MarketingYearCode>>(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status200OK, statusCode);
        Assert.NotEmpty(body);
        Assert.Equal("2022", body[0].Code);
        Assert.Equal("2022 scheme year", body[0].Description);

        Assert.Equal("2021", body[1].Code);
        Assert.Equal("2021 scheme year", body[1].Description);

        Assert.Equal("2020", body[2].Code);
        Assert.Equal("2020 scheme year", body[2].Description);
    }

    [Theory]
    [InlineData("AP", "EA", "LS", null)]
    [InlineData("AP", "EA", null, null)]
    [InlineData("AP", null, null, null)]
    [InlineData(null, null, null, null)]
    public async Task GetWithMissingParametersShouldReturn400BadRequest(string? invoiceType, string? deliveryBody, string? schemeType, string? paymentType)
    {
        // Act
        var result = await MarketingYearEndpoints.GetYears(invoiceType, deliveryBody, schemeType, paymentType, _mockRouteService.Object, _mockDataService.Object);

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
        var result = await MarketingYearEndpoints.GetYears("NA", "NA", "NA", "NA", _mockRouteService.Object, _mockDataService.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, statusCode);
    }

    [Fact]
    public async Task GetShouldReturn204NoContentIfNoYearCodes()
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

        var mockYearCodes = Array.Empty<MarketingYearCode>();

        _mockDataService.Setup(x => x.Get(mockRoute)).ReturnsAsync(mockYearCodes);

        // Act
        var result = await MarketingYearEndpoints.GetYears("AP", "EA", "CS", "DOM", _mockRouteService.Object, _mockDataService.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, statusCode);
    }
}
