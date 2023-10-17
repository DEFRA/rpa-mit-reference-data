using Azure.Core;

namespace RPA.MIT.ReferenceData.Api.Authentication;

#pragma warning disable 1591

public static class TokenCache
{
    public static AccessToken? AccessToken { get; set; }
}