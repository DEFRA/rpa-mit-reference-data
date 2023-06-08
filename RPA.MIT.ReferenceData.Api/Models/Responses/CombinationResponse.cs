namespace RPA.MIT.ReferenceData.Api.Models.Responses;

/// <summary>
/// Response model representing a valid CoA combination
/// </summary>
public class CombinationResponse
{
    /// <summary>
    /// Account code component of valid combination
    /// </summary>
    public string AccountCode { get; init; } = default!;

    /// <summary>
    /// Scheme code component of valid combination
    /// </summary>
    public string SchemeCode { get; init; } = default!;

    /// <summary>
    /// Delivery body code component of valid combination
    /// </summary>
    public string DeliveryBodyCode { get; init; } = default!;

    /// <summary>
    /// Creates instance of CombinationResponse
    /// </summary>
    public CombinationResponse(string accountCode, string schemeCode, string deliveryBodyCode)
    {
        AccountCode = accountCode;
        SchemeCode = schemeCode;
        DeliveryBodyCode = deliveryBodyCode;
    }

    /// <summary>
    /// Creates instance of CombinationResponse
    /// </summary>
    public CombinationResponse()
    {
    }
}
