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
        bool seedData = false;

        if (configuration.IsLocalDatabase(configuration))
        {
            context.Database.EnsureCreated();
        }

        if (context.PackageVersion.Count() == 0)
        {
            var packageVersion = new Data.Models.ReferenceDataPackageVersion
            {
                VersionNumber = configuration["MitRefDataVersion"]
            };
            context.PackageVersion.Add(packageVersion);
            seedData = true;
        }
        else
        {
            if (context.PackageVersion.First().VersionNumber != configuration["MitRefDataVersion"])
            {
                seedData = true;
                context.PackageVersion.First().VersionNumber = configuration["MitRefDataVersion"];
            }
        }

        context.SeedData(context.InvoiceTypes, ReadSeedData<InvoiceType>($"{BaseDir}/RouteComponents/invoice-types.json"), seedData);
        context.SeedData(context.Organisations, ReadSeedData<Organisation>($"{BaseDir}/RouteComponents/organisations.json"), seedData);
        context.SeedData(context.SchemeTypes, ReadSeedData<SchemeType>($"{BaseDir}/RouteComponents/scheme-types.json"), seedData);
        context.SeedData(context.PaymentTypes, ReadSeedData<PaymentType>($"{BaseDir}/RouteComponents/payment-types.json"), seedData);

        context.SeedData(context.InvoiceRoutes, RouteSeedData.GetRoutes(context), seedData);

        context.SeedData(context.AccountCodes, ReadSeedData<AccountCode>($"{BaseDir}/Codes/account-codes.json"), seedData);
        context.SeedData(context.SchemeCodes, ReadSeedData<SchemeCode>($"{BaseDir}/Codes/scheme-codes.json"), seedData);
        context.SeedData(context.MarketingYearCodes, ReadSeedData<MarketingYearCode>($"{BaseDir}/Codes/marketing-years.json"), seedData);
        context.SeedData(context.DeliveryBodyCodes, ReadSeedData<DeliveryBodyCode>($"{BaseDir}/Codes/delivery-body-codes.json"), seedData);
        context.SeedData(context.FundCodes, ReadSeedData<FundCode>($"{BaseDir}/Codes/fund-codes.json"), seedData);

        RouteSeedData.AddRouteCodes(context, seedData);

        sw.Stop();
    }

    /// <summary>
    /// SeedData
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    /// <param name="context">context</param>
    /// <param name="entity">entity</param>
    /// <param name="data">data</param>
    /// <param name="seedData">seedData</param>
    public static void SeedData<T>(this DbContext context, DbSet<T> entity, IEnumerable<T> data, bool seedData)
        where T : class
    {
        if (seedData)
        {
            foreach (var existingEntity in entity)
            {
                entity.Remove(existingEntity);
            }

            entity.AddRange(data);

            context.SaveChanges();
        }
    }

    private static IEnumerable<T> ReadSeedData<T>(string path)
    {
        var raw = File.ReadAllText(Path.Combine(ExecutionPath, path));

        return JsonSerializer.Deserialize<IEnumerable<T>>(raw)!;
    }
}
