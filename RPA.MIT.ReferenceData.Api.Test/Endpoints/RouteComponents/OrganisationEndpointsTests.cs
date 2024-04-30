using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using RPA.MIT.ReferenceData.Api.Endpoints.RouteComponents;
using RPA.MIT.ReferenceData.Api.Exceptions;
using RPA.MIT.ReferenceData.Api.Interfaces;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Api.Test.Endpoints.RouteComponents;

public class OrganisationEndpointsTests
{
    private readonly Mock<IOrganisationDataService> _mockDataService = new();

    [Fact]
    public async Task GetShouldReturn200OkIfOrganisationsFound()
    {
        // Arrange
        var mockOrgs = new Organisation[]
        {
            new("EA", "Environment Agency"),
            new("NE", "Natural England"),
            new("RPA", "Rural Payments Agency")
        };

        _mockDataService.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(mockOrgs);

        // Act
        var result = await OrganisationEndpoints.GetOrganisations(null, _mockDataService.Object, new NullLoggerFactory());

        var (statusCode, body) = await Utilites.GetResponse<List<Organisation>>(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status200OK, statusCode);
        Assert.NotEmpty(body);
        Assert.Equal("EA", body[0].Code);
        Assert.Equal("Environment Agency", body[0].Description);

        Assert.Equal("NE", body[1].Code);
        Assert.Equal("Natural England", body[1].Description);

        Assert.Equal("RPA", body[2].Code);
        Assert.Equal("Rural Payments Agency", body[2].Description);
    }

    [Fact]
    public async Task GetShouldReturn404NotFoundIfNoOrganisationsFound()
    {
        // Arrange
        var mockOrgs = Array.Empty<Organisation>();

        _mockDataService.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(mockOrgs);

        // Act
        var result = await OrganisationEndpoints.GetOrganisations(null, _mockDataService.Object, new NullLoggerFactory());

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

        _mockDataService.Setup(x => x.Get(It.IsAny<string>()))
            .ThrowsAsync(new InvoiceTypeNotFoundException("Invoice type (ZZ) code not found in reference data."));

        // Act
        var result = await OrganisationEndpoints.GetOrganisations("ZZ", _mockDataService.Object, mockLogFactory.Object);

        var statusCode = await Utilites.GetStatusCode(result);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, statusCode);

        mockLogger.Verify(
            x => x.Log(
                It.Is<LogLevel>(level => level == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((obj, _) => obj.ToString() == "Error retrieving organisation type codes: Invoice type (ZZ) code not found in reference data."),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((_, _) => true)),
            Times.Once);
    }
}
