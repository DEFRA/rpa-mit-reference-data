using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using RPA.MIT.ReferenceData.Api.Endpoints.RouteComponents;
using RPA.MIT.ReferenceData.Api.Exceptions;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Test.Endpoints.RouteComponents;

public class PaymentTypeEndpointsTests
{
    private readonly Mock<IPaymentTypeDataService> _mockDataService = new();

    [Fact]
    public async Task GetShouldReturn200OkIfPaymentTypesFound()
    {
        // Arrange
        var mockTypes = new PaymentType[]
        {
            new("EU", "European Union"),
            new("DOM", "Domestic")
        };

        _mockDataService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(mockTypes);

        // Act
        var result = await PaymentTypeEndpoints.GetPaymentTypes(null, null, null, _mockDataService.Object, new NullLoggerFactory());

        var (statusCode, body) = await Utilites.GetResponse<List<PaymentType>>(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status200OK, statusCode);
        Assert.NotEmpty(body);
        Assert.Contains(body, type => "EU" == type.Code && type.Description == "European Union");
        Assert.Contains(body, type => "DOM" == type.Code && type.Description == "Domestic");
    }

    [Fact]
    public async Task GetShouldReturn404NotFoundIfNoPaymentTypesFound()
    {
        // Arrange
        var mockTypes = Array.Empty<PaymentType>();

        _mockDataService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(mockTypes);

        // Act
        var result = await PaymentTypeEndpoints.GetPaymentTypes(null, null, null, _mockDataService.Object, new NullLoggerFactory());

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

        _mockDataService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new InvoiceTypeNotFoundException("Invoice type (ZZ) code not found in reference data."));

        // Act
        var result = await PaymentTypeEndpoints.GetPaymentTypes("ZZ", null, null, _mockDataService.Object, mockLogFactory.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCode);

        mockLogger.Verify(
            x => x.Log(
                It.Is<LogLevel>(level => level == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((obj, _) => obj.ToString() == "Error retrieving payment type codes: Invoice type (ZZ) code not found in reference data."),
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

        _mockDataService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new DeliveryBodyNotFoundException("Delivery body (ZZ) code not found in reference data."));

        // Act
        var result = await PaymentTypeEndpoints.GetPaymentTypes("AP", "ZZ", null, _mockDataService.Object, mockLogFactory.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCode);

        mockLogger.Verify(
            x => x.Log(
                It.Is<LogLevel>(level => level == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((obj, _) => obj.ToString() == "Error retrieving payment type codes: Delivery body (ZZ) code not found in reference data."),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((_, _) => true)),
            Times.Once);
    }

    [Fact]
    public async Task GetShouldReturn400IfPaymentTypeExceptionThrown()
    {
        // Arrange
        var mockLogFactory = new Mock<ILoggerFactory>();
        var mockLogger = new Mock<ILogger>();

        mockLogFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(mockLogger.Object);

        _mockDataService.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new SchemeTypeNotFoundException("Payment type (ZZ) code not found in reference data."));

        // Act
        var result = await PaymentTypeEndpoints.GetPaymentTypes("AP", "EA", "ZZ", _mockDataService.Object, mockLogFactory.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCode);

        mockLogger.Verify(
            x => x.Log(
                It.Is<LogLevel>(level => level == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((obj, _) => obj.ToString() == "Error retrieving payment type codes: Payment type (ZZ) code not found in reference data."),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((_, _) => true)),
            Times.Once);
    }
}
