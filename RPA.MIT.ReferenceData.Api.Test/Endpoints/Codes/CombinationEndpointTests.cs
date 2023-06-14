using Microsoft.AspNetCore.Http;
using Moq;
using RPA.MIT.ReferenceData.Api.Endpoints.Codes;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Api.Models.Responses;
using RPA.MIT.ReferenceData.Data.Models;

namespace RPA.MIT.ReferenceData.Api.Test.Endpoints.Codes;

public class CombinationEndpointTests
{
    private readonly Mock<IInvoiceRouteService> _mockRouteService = new();
    private readonly Mock<ICombinationDataService> _mockDataService = new();

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

        var mockCombinations = new CombinationResponse[]
        {
            new("SOS130", "12345", "TD00"),
            new("SOS130", "12346", "TD01"),
            new("SOS130", "12347", "TD01"),
        };

        _mockDataService.Setup(x => x.Get(mockRoute)).ReturnsAsync(mockCombinations);

        // Act
        var result = await CombinationEndpoints.GetCombinations("AP", "EA", "CS", "DOM", _mockRouteService.Object, _mockDataService.Object);

        var (statusCode, body) = await Utilites.GetResponse<CombinationResponse[]>(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status200OK, statusCode);
        Assert.NotEmpty(body);
        Assert.Equal("SOS130", body[0].AccountCode);
        Assert.Equal("12345", body[0].SchemeCode);
        Assert.Equal("TD00", body[0].DeliveryBodyCode);

        Assert.Equal("SOS130", body[1].AccountCode);
        Assert.Equal("12346", body[1].SchemeCode);
        Assert.Equal("TD01", body[1].DeliveryBodyCode);

        Assert.Equal("SOS130", body[2].AccountCode);
        Assert.Equal("12347", body[2].SchemeCode);
        Assert.Equal("TD01", body[2].DeliveryBodyCode);
    }

    [Theory]
    [InlineData("AP", "EA", "LS", null)]
    [InlineData("AP", "EA", null, null)]
    [InlineData("AP", null, null, null)]
    [InlineData(null, null, null, null)]
    public async Task GetWithMissingParametersShouldReturn400BadRequest(string? invoiceType, string? deliveryBody, string? schemeType, string? paymentType)
    {
        // Act
        var result = await CombinationEndpoints.GetCombinations(invoiceType, deliveryBody, schemeType, paymentType, _mockRouteService.Object, _mockDataService.Object);

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
        var result = await CombinationEndpoints.GetCombinations("NA", "NA", "NA", "NA", _mockRouteService.Object, _mockDataService.Object);

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

        var mockAccounts = Array.Empty<CombinationResponse>();

        _mockDataService.Setup(x => x.Get(mockRoute)).ReturnsAsync(mockAccounts);

        // Act
        var result = await CombinationEndpoints.GetCombinations("AP", "EA", "CS", "DOM", _mockRouteService.Object, _mockDataService.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, statusCode);
    }
}
