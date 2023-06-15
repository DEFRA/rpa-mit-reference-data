using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Extensions;
using RPA.MIT.ReferenceData.Data;

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

app.UseReferenceDataEndpoints();
app.UseSwaggerEndpoints();

app.Run();
