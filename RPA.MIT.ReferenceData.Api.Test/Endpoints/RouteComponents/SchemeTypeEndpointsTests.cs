using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using RPA.MIT.ReferenceData.Api.Endpoints.RouteComponents;
using RPA.MIT.ReferenceData.Api.Exceptions;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Test.Endpoints.RouteComponents;

public class SchemeTypeEndpointsTests
{
    private readonly Mock<ISchemeTypeDataService> _mockDataService = new();

    [Fact]
    public async Task GetShouldReturn200OkIfSchemeTypesFound()
    {
        // Arrange
        var mockTypes = new SchemeType[]
        {
            new("CS", "Current"),
            new("LS", "Legacy"),
            new("DOM", "Domestic")
        };

        _mockDataService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(mockTypes);

        // Act
        var result = await SchemeTypeEndpoints.GetSchemeTypes(null, null, _mockDataService.Object, new NullLoggerFactory());

        var (statusCode, body) = await Utilites.GetResponse<List<SchemeType>>(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status200OK, statusCode);
        Assert.NotEmpty(body);
        Assert.Equal("CS", body[0].Code);
        Assert.Equal("Current", body[0].Description);

        Assert.Equal("LS", body[1].Code);
        Assert.Equal("Legacy", body[1].Description);

        Assert.Equal("DOM", body[2].Code);
        Assert.Equal("Domestic", body[2].Description);
    }

    [Fact]
    public async Task GetShouldReturn404NotFoundIfNoSchemeTypesFound()
    {
        // Arrange
        var mockTypes = Array.Empty<SchemeType>();

        _mockDataService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(mockTypes);

        // Act
        var result = await SchemeTypeEndpoints.GetSchemeTypes(null, null, _mockDataService.Object, new NullLoggerFactory());

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, statusCode);
    }

    [Fact]
    public async Task GetShouldReturn400IfInvoiceTypeExceptionThrown()
    {
        // Arrange
        var mockLogFactory = new Mock<ILoggerFactory>();
        var mockLogger = new Mock<ILogger>();

        mockLogFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(mockLogger.Object);

        _mockDataService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new InvoiceTypeNotFoundException("Invoice type (ZZ) code not found in reference data."));

        // Act
        var result = await SchemeTypeEndpoints.GetSchemeTypes("ZZ", null, _mockDataService.Object, mockLogFactory.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCode);

        mockLogger.Verify(
            x => x.Log(
                It.Is<LogLevel>(level => level == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((obj, _) => obj.ToString() == "Error retrieving scheme type codes: Invoice type (ZZ) code not found in reference data."),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((_, _) => true)),
            Times.Once);
    }

    [Fact]
    public async Task GetShouldReturn400IfDeliveryBodyExceptionThrown()
    {
        // Arrange
        var mockLogFactory = new Mock<ILoggerFactory>();
        var mockLogger = new Mock<ILogger>();

        mockLogFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(mockLogger.Object);

        _mockDataService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new DeliveryBodyNotFoundException("Delivery body (ZZ) code not found in reference data."));

        // Act
        var result = await SchemeTypeEndpoints.GetSchemeTypes("AP", "ZZ", _mockDataService.Object, mockLogFactory.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCode);

        mockLogger.Verify(
            x => x.Log(
                It.Is<LogLevel>(level => level == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((obj, _) => obj.ToString() == "Error retrieving scheme type codes: Delivery body (ZZ) code not found in reference data."),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((_, _) => true)),
            Times.Once);
    }
}
