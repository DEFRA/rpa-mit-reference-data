using Azure.Core;

namespace RPA.MIT.ReferenceData.Api.Authentication;

public interface ITokenGenerator
{
    Task<AccessToken> GetTokenAsync(string postgresSqlAAD, CancellationToken cancellationToken = default);
}