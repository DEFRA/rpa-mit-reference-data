using System.Diagnostics.CodeAnalysis;

namespace RPA.MIT.ReferenceData.Data.Models.Codes;

[ExcludeFromCodeCoverage]
public class AccountCode : CodeBase
{
    public string? RecoveryType { get; set; }

    public AccountCode(string code, string description) : base(code, description)
    {
    }

    public AccountCode(string code, string description, string recoveryType) : base(code, description)
    {
        RecoveryType = recoveryType;
    }

    public AccountCode()
    {
    }
}
