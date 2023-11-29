namespace RPA.MIT.ReferenceData.Api;

/// <summary>
/// ConfigurationHelper
/// </summary>
public static class ConfigurationHelper
{
    /// <summary>
    /// IsLocalDatabase
    /// </summary>
    /// <param name="c">c</param>
    /// <param name="configuration">configuration</param>
    /// <returns></returns>
    public static bool IsLocalDatabase(this IConfiguration c, IConfiguration configuration)
    {
        var host = configuration["POSTGRES_HOST"];
        return string.IsNullOrEmpty(host) || host.ToLower() == "host.docker.internal" || host.ToLower() == "localhost" || host == "127.0.0.1";
    }
}