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
    /// <summary>
    /// SeedReferenceData
    /// </summary>
    /// <param name="context">context</param>
    /// <param name="configuration">configuration</param>
    /// <param name="scriptWriter">scriptWriter</param>
    /// <param name="version">version of seed data</param>
    public static void SeedReferenceData(ReferenceDataContext context, IConfiguration configuration, SQLscriptWriter? scriptWriter, string version)
    {
        var sw = Stopwatch.StartNew();

        if (configuration.IsLocalDatabase(configuration))
        {
            // If prod allow LiquiBase to perform schema setup.

            // 1) ensure nuget.exe is downloaded installed (placed in system32)
            // 2) from cmd prompt, navigate to the solution folder, e.g.
            //        cd C:\Users\<userid>\source\repos\DEFRA\rpa-mit-reference-data
            // 3) at the command prompt, run:
            //        nuget install "RPA.MIT.ReferenceData" - source "DEFRA-EST" - version "1.0.5" (Where version is the package version)
            // this should install the package files to
            //        C:\Users\<userid>\source\repos\DEFRA\rpa-mit-reference-data\RPA.MIT.ReferenceData.1.0.5
            // the BaseDir is set to load the seed data from there.
            // 4) After seeding locally, the database should be created and populated and the SQL scripts to be run on the server should be saved to:
            //        C:\Users\<userid>\source\repos\DEFRA\rpa-mit-reference-data\RPA.MIT.ReferenceData.Api

            var BaseDirRelativeToExecutionPath = $"..\\..\\..\\..\\RPA.MIT.ReferenceData.{version}\\contentFiles\\any\\any\\Resources\\SeedData";

            var ExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

            var BaseDir = Path.Combine(ExecutionPath, BaseDirRelativeToExecutionPath);

            using (scriptWriter)
            {
                scriptWriter?.Open(version);

                object[] parameters = Array.Empty<object>();
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS account_code_invoice_route", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS combinations", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS delivery_body_code_invoice_route", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS delivery_body_codes", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS fund_code_invoice_route", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS invoice_route_marketing_year_code", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS invoice_route_scheme_code", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS invoice_routes", parameters);

                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS account_codes", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS fund_codes", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS invoice_types", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS marketing_year_codes", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS organisations", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS payment_types", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS scheme_codes", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS scheme_types", parameters);

                context.Database.EnsureCreated();

                context.SeedData(context.InvoiceTypes, ReadSeedData<InvoiceType>($"{BaseDir}/RouteComponents/invoice-types.json"));
                context.SeedData(context.Organisations, ReadSeedData<Organisation>($"{BaseDir}/RouteComponents/organisations.json"));
                context.SeedData(context.SchemeTypes, ReadSeedData<SchemeType>($"{BaseDir}/RouteComponents/scheme-types.json"));
                context.SeedData(context.PaymentTypes, ReadSeedData<PaymentType>($"{BaseDir}/RouteComponents/payment-types.json"));

                context.SeedData(context.InvoiceRoutes, RouteSeedData.GetRoutes(context, $"{BaseDir}/valid-routes.json"));

                context.SeedData(context.AccountCodes, ReadSeedData<AccountCode>($"{BaseDir}/Codes/account-codes.json"));
                context.SeedData(context.SchemeCodes, ReadSeedData<SchemeCode>($"{BaseDir}/Codes/scheme-codes.json"));
                context.SeedData(context.MarketingYearCodes, ReadSeedData<MarketingYearCode>($"{BaseDir}/Codes/marketing-years.json"));
                context.SeedData(context.DeliveryBodyCodes, ReadSeedData<DeliveryBodyCode>($"{BaseDir}/Codes/delivery-body-codes.json"));
                context.SeedData(context.FundCodes, ReadSeedData<FundCode>($"{BaseDir}/Codes/fund-codes.json"));

                RouteSeedData.AddRouteCodes(context, $"{BaseDir}/route-code-mapping.json");

                scriptWriter?.Close();
            }
        }

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
        if (data is null || entity.Any()) return;

        entity.AddRange(data);

        context.SaveChanges();
    }

    private static IEnumerable<T> ReadSeedData<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            var raw = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<IEnumerable<T>>(raw)!;
        }
        return null!;
    }
}
