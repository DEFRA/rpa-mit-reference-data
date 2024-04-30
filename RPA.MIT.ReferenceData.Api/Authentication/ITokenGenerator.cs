using Azure.Core;

namespace RPA.MIT.ReferenceData.Api.Authentication;

#pragma warning disable 1591

public interface ITokenGenerator
{
    Task<AccessToken> GetTokenAsync(string postgresSqlAAD, CancellationToken cancellationToken = default);
}