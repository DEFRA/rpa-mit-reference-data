using Azure.Core;
using Azure.Core.Diagnostics;
using Azure.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace RPA.MIT.ReferenceData.Api;

public class AadAuthenticationInterceptor : DbConnectionInterceptor
{
    private readonly IConfiguration _configuration;
    private readonly bool _isProd; 

    public static class TokenCache
    {
        public static AccessToken? AccessToken { get; set; }
    }

    public AadAuthenticationInterceptor(IConfiguration configuration, bool isProd)
    {
        _configuration = configuration;
        _isProd = isProd;
    }

    public override InterceptionResult ConnectionOpening(
        DbConnection connection,
        ConnectionEventData eventData,
        InterceptionResult result)
        => throw new InvalidOperationException("Open connections asynchronously when using AAD authentication.");

    public override async ValueTask<InterceptionResult> ConnectionOpeningAsync(
        DbConnection connection,
        ConnectionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        connection.ConnectionString = await GetConnectionStringAsync(cancellationToken);

        return result;
    }

    public async Task<string> GetConnectionStringAsync(CancellationToken cancellationToken = default)
    {
        var host = _configuration["POSTGRES_HOST"];
        var port = _configuration["POSTGRES_PORT"];
        var db = _configuration["POSTGRES_DB"];
        var user = _configuration["POSTGRES_USER"];
        var pass = _configuration["POSTGRES_PASSWORD"];
        var postgresSqlAAD = _configuration["AzureADPostgreSQLResourceID"];

        if ((_isProd) && ((!TokenCache.AccessToken.HasValue) || (DateTime.UtcNow >= TokenCache.AccessToken.Value.ExpiresOn)))
        {
            using AzureEventSourceListener listener = AzureEventSourceListener.CreateConsoleLogger();

            var options = new DefaultAzureCredentialOptions()
            {
                Diagnostics =
                {
                    LoggedHeaderNames = { "x-ms-request-id" },
                    LoggedQueryParameters = { "api-version" },
                    IsAccountIdentifierLoggingEnabled = true,
                    IsDistributedTracingEnabled = true,
                    IsLoggingContentEnabled = true
                }
            };

            options.Retry.NetworkTimeout = TimeSpan.FromSeconds(1000);

            var sqlServerTokenProvider = new DefaultAzureCredential(options);
            TokenCache.AccessToken = await sqlServerTokenProvider
                .GetTokenAsync(new TokenRequestContext(scopes: new string[] { postgresSqlAAD! }) { }, cancellationToken);
            pass = TokenCache.AccessToken.Value.Token;
        }

        return string.Format(_configuration["DbConnectionTemplate"]!, host, port, db, user, pass);
    }
}