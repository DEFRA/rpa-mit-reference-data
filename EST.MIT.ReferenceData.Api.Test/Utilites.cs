using System.Text.Json;
using EST.MIT.ReferenceData.Data;
using EST.MIT.ReferenceData.Data.Models;
using EST.MIT.ReferenceData.Data.Models.RouteComponents;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EST.MIT.ReferenceData.Api.Test;

public static class Utilites
{
    public static async Task<(int statusCode, T body)> GetResponse<T>(IResult result)
    {
        var mockHttpContext = new DefaultHttpContext
        {
            RequestServices = new ServiceCollection().AddLogging().BuildServiceProvider(),
            Response =
            {
                Body = new MemoryStream()
            }
        };

        await result.ExecuteAsync(mockHttpContext);

        mockHttpContext.Response.Body.Position = 0;

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        var obj = await JsonSerializer.DeserializeAsync<T>(mockHttpContext.Response.Body, options);

        if (obj is null)
            throw new InvalidOperationException();

        return (mockHttpContext.Response.StatusCode, obj);
    }

    public static async Task<int> GetStatusCode(IResult result)
    {
        var mockHttpContext = new DefaultHttpContext
        {
            RequestServices = new ServiceCollection().AddLogging().BuildServiceProvider(),
            Response =
            {
                Body = new MemoryStream(),
            }
        };

        await result.ExecuteAsync(mockHttpContext);

        return mockHttpContext.Response.StatusCode;
    }

    public static InvoiceRoute CreateInvoiceRoute(string invoiceType, string organisation, string schemeType, string paymentType)
    {
        var it = new InvoiceType(invoiceType, invoiceType);
        var org = new Organisation(organisation, organisation);
        var st = new SchemeType(schemeType, schemeType);
        var py = new PaymentType(paymentType, paymentType);

        return new InvoiceRoute(it, org, st, py);
    }

    public static InvoiceRoute CreateInvoiceRoute(ReferenceDataContext context, string invoiceType, string organisation, string schemeType, string paymentType)
    {
        return new InvoiceRoute(
            context.InvoiceTypes.Single(x => x.Code == invoiceType),
            context.Organisations.Single(x => x.Code == organisation),
            context.SchemeTypes.Single(x => x.Code == schemeType),
            context.PaymentTypes.Single(x => x.Code == paymentType)
        );
    }

    public static ReferenceDataContext GetRefDataContext()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        var builder = new DbContextOptionsBuilder<ReferenceDataContext>();

        builder.UseSqlite(connection);

        var context = new ReferenceDataContext(builder.Options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }
}