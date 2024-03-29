using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api;
using RPA.MIT.ReferenceData.Api.Authentication;
using RPA.MIT.ReferenceData.Api.Extensions;
using RPA.MIT.ReferenceData.Api.SeedData;
using RPA.MIT.ReferenceData.Data;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

SQLscriptWriter? _sqlScriptWriter = default;
CreateAndInsertSqlCommandInterceptor? createAndInsertSQLCommandInterceptor = default;

var isLocalDev = builder.Configuration.IsLocalDatabase(builder.Configuration);

if (isLocalDev)
{
    _sqlScriptWriter = new SQLscriptWriter($"MIT_ReferenceData_Seed_SQL_v{{version}}_{DateTime.Now.ToString("yyyyMMdd-HHmm")}.sql");
    createAndInsertSQLCommandInterceptor = new CreateAndInsertSqlCommandInterceptor(_sqlScriptWriter);
}

var aadAuthenticationInterceptor = new AadAuthenticationInterceptor(new TokenGenerator(), builder.Configuration);

builder.Services.AddDbContext<ReferenceDataContext>(options =>
{
    var connStringTask = aadAuthenticationInterceptor.GetConnectionStringAsync();
    var connString = connStringTask.GetAwaiter().GetResult();

    options.AddInterceptors(aadAuthenticationInterceptor);
    if (isLocalDev && createAndInsertSQLCommandInterceptor is not null)
    {
        options.AddInterceptors(createAndInsertSQLCommandInterceptor);
    }

    options.UseNpgsql(
        connString,
        x => x.MigrationsAssembly("RPA.MIT.ReferenceData.Data")
    )
    .UseSnakeCaseNamingConvention();
});

builder.Services.AddSwaggerServices();
builder.Services.AddReferenceDataServices();

var app = builder.Build();

app.UseReferenceDataEndpoints();
app.UseSwaggerEndpoints();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

    SeedProvider.SeedReferenceData(db, builder.Configuration, _sqlScriptWriter, "1.0.5");
}

app.Run();
