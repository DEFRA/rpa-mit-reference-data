using EST.MIT.ReferenceData.Api.Extensions;
using EST.MIT.ReferenceData.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReferenceDataContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration["DbConnectionString"],
        x => x.MigrationsAssembly("EST.MIT.ReferenceData.Data")
    )
    .UseSnakeCaseNamingConvention();
});

builder.Services.AddSwaggerServices();
builder.Services.AddReferenceDataServices();

var app = builder.Build();

if (app.Environment.IsDevelopment() && args.Contains("--seed-ref-data"))
{
    app.UseReferenceDataSeeding();
}

app.UseReferenceDataEndpoints();
app.UseSwaggerEndpoints();

app.Run();
