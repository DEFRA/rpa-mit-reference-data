using Azure.Core;

namespace RPA.MIT.ReferenceData.Api.Authentication;

public static class TokenCache
{
    public static AccessToken? AccessToken { get; set; }
}