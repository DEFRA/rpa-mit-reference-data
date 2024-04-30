using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using RPA.MIT.ReferenceData.Data.Models.Codes;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Data.Models;

[ExcludeFromCodeCoverage]
public class InvoiceRoute
{
    [Key]
    public int RouteId { get; set; }

    public int InvoiceTypeId { get; set; }
    public int OrganisationId { get; set; }
    public int SchemeTypeId { get; set; }
    public int PaymentTypeId { get; set; }

    public InvoiceType InvoiceType { get; set; } = default!;
    public Organisation Organisation { get; set; } = default!;
    public SchemeType SchemeType { get; set; } = default!;
    public PaymentType PaymentType { get; set; } = default!;

    public ICollection<SchemeCode> SchemeCodes { get; set; } = default!;
    public ICollection<AccountCode> AccountCodes { get; set; } = default!;
    public ICollection<FundCode> FundCodes { get; set; } = default!;
    public ICollection<DeliveryBodyCode> DeliveryBodyCodes { get; set; } = default!;
    public ICollection<MarketingYearCode> MarketingYearCodes { get; set; } = default!;
    public ICollection<Combination> Combinations { get; set; } = default!;

    public InvoiceRoute(InvoiceType invoiceType, Organisation organisation, SchemeType schemeType, PaymentType paymentType)
    {
        InvoiceType = invoiceType;
        Organisation = organisation;
        SchemeType = schemeType;
        PaymentType = paymentType;
    }

    public override string ToString()
    {
        return $"/{InvoiceType.Code}/{Organisation.Code}/{SchemeType.Code}/{PaymentType.Code}";
    }

    private InvoiceRoute()
    {
    }
}
