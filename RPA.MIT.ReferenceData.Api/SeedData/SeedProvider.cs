using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models.Codes;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;

namespace RPA.MIT.ReferenceData.Api.SeedData;

/// <summary>
/// SeedProvider
/// </summary>
[ExcludeFromCodeCoverage]
public static class SeedProvider
{
    private const string BaseDir = "Resources/SeedData";
    private static readonly string ExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

    /// <summary>
    /// SeedReferenceData
    /// </summary>
    /// <param name="context">context</param>
    /// <param name="configuration">configuration</param>
    public static void SeedReferenceData(ReferenceDataContext context, IConfiguration configuration)
    {
        var sw = Stopwatch.StartNew();

        if (configuration.IsLocalDatabase(configuration))
        {
            // If prod allow LiquiBase to perform schema setup.
            context.Database.EnsureCreated();
        }

        context.SeedData(context.InvoiceTypes, ReadSeedData<InvoiceType>($"{BaseDir}/RouteComponents/invoice-types.json"));
        context.SeedData(context.Organisations, ReadSeedData<Organisation>($"{BaseDir}/RouteComponents/organisations.json"));
        context.SeedData(context.SchemeTypes, ReadSeedData<SchemeType>($"{BaseDir}/RouteComponents/scheme-types.json"));
        context.SeedData(context.PaymentTypes, ReadSeedData<PaymentType>($"{BaseDir}/RouteComponents/payment-types.json"));

        context.SeedData(context.InvoiceRoutes, RouteSeedData.GetRoutes(context));

        context.SeedData(context.AccountCodes, ReadSeedData<AccountCode>($"{BaseDir}/Codes/account-codes.json"));
        context.SeedData(context.SchemeCodes, ReadSeedData<SchemeCode>($"{BaseDir}/Codes/scheme-codes.json"));
        context.SeedData(context.MarketingYearCodes, ReadSeedData<MarketingYearCode>($"{BaseDir}/Codes/marketing-years.json"));
        context.SeedData(context.DeliveryBodyCodes, ReadSeedData<DeliveryBodyCode>($"{BaseDir}/Codes/delivery-body-codes.json"));
        context.SeedData(context.FundCodes, ReadSeedData<FundCode>($"{BaseDir}/Codes/fund-codes.json"));

        RouteSeedData.AddRouteCodes(context);

        sw.Stop();
    }

    /// <summary>
    /// SeedData
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// <param name="context">context</param>
    /// <param name="entity">entity</param>
    /// <param name="data">data</param>
    public static void SeedData<T>(this DbContext context, DbSet<T> entity, IEnumerable<T> data)
        where T : class
    {
        if (entity.Any()) return;

        entity.AddRange(data);

        context.SaveChanges();
    }

    private static IEnumerable<T> ReadSeedData<T>(string path)
    {
        var raw = File.ReadAllText(Path.Combine(ExecutionPath, path));

        return JsonSerializer.Deserialize<IEnumerable<T>>(raw)!;
    }
}
