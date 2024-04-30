using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RPA.MIT.ReferenceData.Data.Models.RouteComponents;

[ExcludeFromCodeCoverage]
public abstract class RouteComponentBase
{
    [Key]
    [JsonIgnore]
    public int ComponentId { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }

    protected RouteComponentBase(string code, string description)
    {
        Code = code;
        Description = description;
    }
}
