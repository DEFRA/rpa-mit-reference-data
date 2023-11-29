using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RPA.MIT.ReferenceData.Api.Authentication;
using RPA.MIT.ReferenceData.Api.Extensions;
using RPA.MIT.ReferenceData.Api.SeedData;
using RPA.MIT.ReferenceData.Data;

var builder = WebApplication.CreateBuilder(args);

var interceptor = new AadAuthenticationInterceptor(new TokenGenerator(), builder.Configuration);

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


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

    SeedProvider.SeedReferenceData(db, builder.Configuration);
}

app.Run();
