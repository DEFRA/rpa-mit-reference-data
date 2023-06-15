using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models.Codes;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.SeedProvider.SeedData;

[ExcludeFromCodeCoverage]
public static class SeedProvider
{
    private const string BaseDir = "Resources/SeedData";
    private static readonly string ExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

    public static void SeedReferenceData(ReferenceDataContext context, ILogger logger)
    {
        var sw = Stopwatch.StartNew();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

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

        logger.LogInformation("Seeding reference data completed in {elasped} seconds", sw.Elapsed.Seconds);
    }

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
