using Microsoft.Extensions.Configuration;
using Moq;
using RPA.MIT.ReferenceData.Api.Authentication;

namespace RPA.MIT.ReferenceData.Api.Test.Authentication;

public class AadAuthenticationInterceptorTests
{
    private readonly IConfiguration _config;

    public AadAuthenticationInterceptorTests()
    {
        var inMemorySettings = new Dictionary<string, string?> {
            {"POSTGRES_HOST", "ahost.com"},
            {"POSTGRES_PORT", "5432"},
            {"POSTGRES_DB", "a_database"},
            {"POSTGRES_USER", "a_user"},
            {"POSTGRES_PASSWORD", "a_password"},
            {"AzureADPostgreSQLResourceID", "https://ossrdbms-aad.database.windows.net/.default"},
            {"DbConnectionTemplate", "Server={0};Port={1};Database={2};User Id={3};Password={4}"}
        };

        _config = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }

    [Fact]
    public async Task GetConnectionStringAsync_NoTokenInCache_ReturnsConnectionString()
    {
        Mock<ITokenGenerator> mockTokenService = new();
        var connectionInterceptor = new AadAuthenticationInterceptor(mockTokenService.Object, _config, true);
        var cancelToken = new CancellationToken();
        SetUpTokenService(mockTokenService);

        string connectionString = await connectionInterceptor.GetConnectionStringAsync(cancelToken);

        mockTokenService.Verify(d => d.GetTokenAsync("https://ossrdbms-aad.database.windows.net/.default", cancelToken), Times.Once);
        Assert.Equal("Server=ahost.com;Port=5432;Database=a_database;User Id=a_user;Password=a_token", connectionString);
        TokenCache.AccessToken = null;
    }

    [Fact]
    public async Task GetConnectionStringAsync_ExpiredTokenInCache_ReturnsConnectionString()
    {
        Mock<ITokenGenerator> mockTokenService = new();
        var connectionInterceptor = new AadAuthenticationInterceptor(mockTokenService.Object, _config, true);
        var cancelToken = new CancellationToken();

        SetUpTokenService(mockTokenService);

        // Create new token.
        await connectionInterceptor.GetConnectionStringAsync(cancelToken);

        // Expire the token
        TokenCache.AccessToken = new Azure.Core.AccessToken("a_token", DateTime.Now.AddDays(-1));

        // Create another new token as token has expired.
        string connectionString = await connectionInterceptor.GetConnectionStringAsync(cancelToken);

        Assert.Equal("Server=ahost.com;Port=5432;Database=a_database;User Id=a_user;Password=a_token", connectionString);
        mockTokenService.Verify(d => d.GetTokenAsync("https://ossrdbms-aad.database.windows.net/.default", cancelToken), Times.Exactly(2));
        TokenCache.AccessToken = null;
    }

    [Fact]
    public async Task GetConnectionStringAsync_ValidTokenInCache_ReturnsConnectionString()
    {
        Mock<ITokenGenerator> mockTokenService = new();
        var connectionInterceptor = new AadAuthenticationInterceptor(mockTokenService.Object, _config, true);
        var cancelToken = new CancellationToken();

        SetUpTokenService(mockTokenService);

        // Create new token.
        await connectionInterceptor.GetConnectionStringAsync(cancelToken);

        // Read from cache i.e. don't create a new token as it's not expired.
        string connectionString = await connectionInterceptor.GetConnectionStringAsync(cancelToken);

        Assert.Equal("Server=ahost.com;Port=5432;Database=a_database;User Id=a_user;Password=a_token", connectionString);
        mockTokenService.Verify(d => d.GetTokenAsync("https://ossrdbms-aad.database.windows.net/.default", cancelToken), Times.Once);
        TokenCache.AccessToken = null;
    }

    [Fact]
    public async Task GetConnectionStringAsync_NotProd_ReturnsConnectionStringWithoutToken()
    {
        Mock<ITokenGenerator> mockTokenService = new();
        var connectionInterceptor = new AadAuthenticationInterceptor(mockTokenService.Object, _config, false);
        var cancelToken = new CancellationToken();
        string connectionString = await connectionInterceptor.GetConnectionStringAsync(cancelToken);

        mockTokenService.Verify(d => d.GetTokenAsync("https://ossrdbms-aad.database.windows.net/.default", cancelToken), Times.Never);
        Assert.Equal("Server=ahost.com;Port=5432;Database=a_database;User Id=a_user;Password=a_password", connectionString);
        TokenCache.AccessToken = null;
    }

    private static void SetUpTokenService(Mock<ITokenGenerator> mockTokenService)
    {
        mockTokenService.Setup(t => t.GetTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Azure.Core.AccessToken("a_token", DateTime.Now.AddDays(1)));
    }
}