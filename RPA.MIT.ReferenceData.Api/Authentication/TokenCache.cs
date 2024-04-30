using Azure.Core;

namespace RPA.MIT.ReferenceData.Api.Authentication;

#pragma warning disable 1591

public static class TokenCache
{
    public static AccessToken? AccessToken 
    {
        get { lock (_sync) { return _accessToken; } }
        set { lock (_sync) { _accessToken = value; } }
    }

    private static AccessToken? _accessToken;
    private static readonly object _sync = new object();
}