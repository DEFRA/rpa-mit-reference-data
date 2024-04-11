using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RPA.MIT.ReferenceData.Data.Models.Codes;

[ExcludeFromCodeCoverage]
public abstract class CodeBase
{
    [Key]
    [JsonIgnore]
    public int CodeId { get; set; }

    public string Code { get; set; } = default!;

    public string Description { get; set; } = default!;

    [JsonIgnore]
    public IEnumerable<InvoiceRoute> Routes { get; set; } = default!;

    protected CodeBase(string code, string description)
    {
        Code = code;
        Description = description;
    }

    protected CodeBase()
    {
    }
}
