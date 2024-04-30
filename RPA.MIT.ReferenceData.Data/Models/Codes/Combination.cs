using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RPA.MIT.ReferenceData.Data.Models.Codes;

[ExcludeFromCodeCoverage]
public class Combination
{
    [Key]
    public int CombinationId { get; set; }

    [ForeignKey(nameof(Route))]
    public int RouteId { get; set; }

    [ForeignKey(nameof(SchemeCode))]
    public int SchemeCodeId { get; set; }

    [ForeignKey(nameof(AccountCode))]
    public int AccountCodeId { get; set; }

    [ForeignKey(nameof(DeliveryBodyCode))]
    public int DeliveryBodyCodeId { get; set; }

    public InvoiceRoute Route { get; set; } = default!;
    public SchemeCode SchemeCode { get; set; } = default!;
    public AccountCode AccountCode { get; set; } = default!;
    public DeliveryBodyCode DeliveryBodyCode { get; set; } = default!;

    public Combination(InvoiceRoute route, AccountCode accountCode, SchemeCode schemeCode, DeliveryBodyCode deliveryBodyCode)
    {
        Route = route;
        SchemeCode = schemeCode;
        AccountCode = accountCode;
        DeliveryBodyCode = deliveryBodyCode;
    }

    private Combination()
    {
    }
}
