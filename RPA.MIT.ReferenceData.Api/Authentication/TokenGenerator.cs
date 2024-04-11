using Azure.Core;
using Azure.Core.Diagnostics;
using Azure.Identity;
using System.Diagnostics.CodeAnalysis;

namespace RPA.MIT.ReferenceData.Api.Authentication;

#pragma warning disable 1591

[ExcludeFromCodeCoverage]
public class TokenGenerator : ITokenGenerator
{
    public async Task<AccessToken> GetTokenAsync(string postgresSqlAAD, CancellationToken cancellationToken = default)
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
        return await sqlServerTokenProvider
            .GetTokenAsync(new TokenRequestContext(scopes: new string[] { postgresSqlAAD! }) { }, cancellationToken);
    }
}