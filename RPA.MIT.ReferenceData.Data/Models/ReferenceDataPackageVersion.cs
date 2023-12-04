using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RPA.MIT.ReferenceData.Data.Models;

[ExcludeFromCodeCoverage]
public class ReferenceDataPackageVersion
{
    [Key]
    public string? VersionNumber { get; set; }

    public ReferenceDataPackageVersion()
    {
    }
}
