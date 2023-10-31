using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Authentication;
using RPA.MIT.ReferenceData.Api.Extensions;
using RPA.MIT.ReferenceData.Data;

var builder = WebApplication.CreateBuilder(args);

var interceptor = new AadAuthenticationInterceptor(new TokenGenerator(), builder.Configuration, builder.Environment.IsProduction());

builder.Services.AddDbContext<ReferenceDataContext>(options =>
{
    var connStringTask = interceptor.GetConnectionStringAsync();
    var connString = connStringTask.GetAwaiter().GetResult();

    options.AddInterceptors(interceptor);

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

if (interceptor.IsLocalDatabase())
{
    // Run migrations if your database is local
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        // db.Database.Migrate(); // Don't do Migration until SeedProvider and API migrations are synced (else this step might break)
    }
}

app.Run();
