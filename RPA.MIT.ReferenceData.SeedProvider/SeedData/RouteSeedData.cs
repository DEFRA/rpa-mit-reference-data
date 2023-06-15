using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;

namespace RPA.MIT.ReferenceData.SeedProvider.SeedData;

#nullable disable

[ExcludeFromCodeCoverage]
public static class RouteSeedData
{
    private const string MappingDefinitionFile = "Resources/SeedData/route-code-mapping.json";

    public static List<InvoiceRoute> GetRoutes(ReferenceDataContext context)
    {
        var routes = new List<InvoiceRoute>();

        var executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        using var stream = File.OpenRead(Path.Combine(executionPath, "Resources/SeedData/valid-routes.json"));
        var json = JsonSerializer.Deserialize<JsonNode>(stream);

        foreach (var route in json.AsArray())
        {
            var it = route[0].ToString();
            var org = route[1].ToString();
            var st = route[2].ToString();
            var pt = route[3].ToString();

            routes.Add(context.CreateRoute(it, org, st, pt));
        }

        return routes;
    }

    public static void AddRouteCodes(ReferenceDataContext context)
    {
        AddRouteCodes(context, MappingDefinitionFile);
    }

    public static void AddRouteCodes(ReferenceDataContext context, string routeJsonPath)
    {
        var executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        using var stream = File.OpenRead(Path.Combine(executionPath, routeJsonPath));
        var json = JsonSerializer.Deserialize<JsonNode>(stream);

        var routes = context.InvoiceRoutes
            .Include(r => r.InvoiceType)
            .Include(r => r.Organisation)
            .Include(r => r.SchemeCodes)
            .Include(r => r.PaymentType)
            .AsSplitQuery()
            .ToArray();

        foreach (var route in routes)
        {
            var it = route.InvoiceType.Code.ToLower();
            var org = route.Organisation.Code.ToLower();
            var st = route.SchemeType.Code.ToLower();
            var pt = route.PaymentType.Code.ToLower();

            var routeJson = json[it][org][st][pt];

            LinkCodes(route, context.InvoiceRoutes, routeJson["schemes"], context.SchemeCodes, r => r.SchemeCodes);
            LinkCodes(route, context.InvoiceRoutes, routeJson["marketingYears"], context.MarketingYearCodes, r => r.MarketingYearCodes);
            LinkCodes(route, context.InvoiceRoutes, routeJson["deliveryBodies"], context.DeliveryBodyCodes, r => r.DeliveryBodyCodes);
            LinkCodes(route, context.InvoiceRoutes, routeJson["funds"], context.FundCodes, r => r.FundCodes);
            LinkCodes(route, context.InvoiceRoutes, routeJson["accounts"], context.AccountCodes, r => r.AccountCodes, AccountCodeMatcher<AccountCode>);

            LinkCombinations(route, context, routeJson["coa"]);
        }

        context.SaveChanges();
    }

    private static InvoiceRoute CreateRoute(this ReferenceDataContext context,
        string typeCode,
        string dbCode,
        string schemeCode,
        string paymentCode)
    {
        var it = context.InvoiceTypes.Single(x => x.Code == typeCode);
        var db = context.Organisations.Single(x => x.Code == dbCode);
        var st = context.SchemeTypes.Single(x => x.Code == schemeCode);
        var pay = context.PaymentTypes.Single(x => x.Code == paymentCode);

        return new InvoiceRoute(it, db, st, pay);
    }

    private static Expression<Func<T, bool>> DefaultCodeMatcher<T>(InvoiceRoute route, HashSet<string> routeCodes) where T : CodeBase
    {
        return c => routeCodes.Contains(c.Code);
    }

    private static Expression<Func<T, bool>> AccountCodeMatcher<T>(InvoiceRoute route, HashSet<string> routeCodes) where T : AccountCode
    {
        return c => routeCodes.Contains(c.Code)
            && (route.InvoiceType.Code == "AP"
                ? c.RecoveryType == null
                : c.RecoveryType != null);
    }

    private static void LinkCodes<T>(InvoiceRoute routeData,
        IQueryable<InvoiceRoute> routes,
        JsonNode codes,
        IQueryable<T> storedCodes,
        Expression<Func<InvoiceRoute, ICollection<T>>> navProperty,
        Func<InvoiceRoute, HashSet<string>, Expression<Func<T, bool>>> matcher = null) where T : CodeBase
    {
        matcher ??= DefaultCodeMatcher<T>;

        var route = routes
            .Include(navProperty)
            .Single(r => r.RouteId == routeData.RouteId);

        var routeCodes = codes.AsArray()
            .Select(x => x.ToString())
            .ToHashSet();

        var matchedCodes = storedCodes.Where(matcher(routeData, routeCodes));

        if (matchedCodes.Count() != routeCodes.Count)
            throw new InvalidOperationException($"Invalid {nameof(T)} in route {route}");

        var selector = navProperty.Compile();

        foreach (var code in matchedCodes)
        {
            selector(route).Add(code);
        }
    }

    private static void LinkCombinations(InvoiceRoute routeData, ReferenceDataContext context, JsonNode routeCombinations)
    {
        var route = context.InvoiceRoutes
            .Include(x => x.Combinations)
            .Single(r => r.RouteId == routeData.RouteId);

        var combinations = routeCombinations.AsArray()
            .Select(c => new
            {
                AccountCode = c[0].ToString(),
                SchemeCode = c[1].ToString(),
                DeliveryBody = c[2].ToString()
            });

        var schemeCodes = context.SchemeCodes.ToDictionary(c => c.Code, c => c);

        var accountCodes = context.AccountCodes
            .Where(c => routeData.InvoiceType.Code == "AP"
                        ? c.RecoveryType == null
                        : c.RecoveryType != null)
            .ToDictionary(c => c.Code, c => c);

        var deliveryBodyCodes = context.DeliveryBodyCodes.ToDictionary(c => c.Code, c => c);

        foreach (var combination in combinations)
        {
            if (combination.AccountCode == "ACC" && combination.SchemeCode == "ALL" && combination.DeliveryBody == "ALL")
                return;

            var scheme = schemeCodes[combination.SchemeCode];
            var account = accountCodes[combination.AccountCode];
            var deliveryBody = deliveryBodyCodes[combination.DeliveryBody];

            var entity = new Combination(route, account, scheme, deliveryBody);

            route.Combinations.Add(entity);
        }
    }
}
