using Azure.Core;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

#pragma warning disable 1591

namespace RPA.MIT.ReferenceData.Api.Authentication;

public class AadAuthenticationInterceptor : DbConnectionInterceptor
{
    private readonly IConfiguration _configuration;
    private readonly ITokenGenerator _tokenService;

    public AadAuthenticationInterceptor(ITokenGenerator tokenService, IConfiguration configuration)
    {
        _configuration = configuration;
        _tokenService = tokenService;
    }

    public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
    {
        // handles connectionString init for Non-async db calls, such as for InvoiceTypeParameterFilter used in AddSwaggerServices
        if (_configuration.IsLocalDatabase(_configuration))
        {
            connection.ConnectionString = GetConnectionString();
        }
        return base.ConnectionOpening(connection, eventData, result);
    }

    public string GetConnectionString()
    {
        var host = _configuration["POSTGRES_HOST"];
        var port = _configuration["POSTGRES_PORT"];
        var db = _configuration["POSTGRES_DB"];
        var user = _configuration["POSTGRES_USER"];
        var pass = _configuration["POSTGRES_PASSWORD"];
        return string.Format(_configuration["DbConnectionTemplate"]!, host, port, db, user, pass);
    }

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

        if (!_configuration.IsLocalDatabase(_configuration))
        {
            if ((!TokenCache.AccessToken.HasValue) || (DateTime.Now >= TokenCache.AccessToken.Value.ExpiresOn.AddMinutes(-1)))
            {
                TokenCache.AccessToken = await _tokenService.GetTokenAsync(postgresSqlAAD!, cancellationToken);
            }

            pass = TokenCache.AccessToken.Value.Token;
        }

        return string.Format(_configuration["DbConnectionTemplate"]!, host, port, db, user, pass);
   }

    public bool IsLocalDatabase()
    {
        var host = _configuration["POSTGRES_HOST"];
        return string.IsNullOrEmpty(host) || host.ToLower() == "host.docker.internal" || host.ToLower() == "localhost" || host == "127.0.0.1";
    }
}