using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Api.Extensions;
using RPA.MIT.ReferenceData.Data;

var builder = WebApplication.CreateBuilder(args);

var host = builder.Configuration["POSTGRES_HOST"];
var port = builder.Configuration["POSTGRES_PORT"];
var db = builder.Configuration["POSTGRES_DB"];
var user = builder.Configuration["POSTGRES_USER"];
var pass = builder.Configuration["POSTGRES_PASSWORD"];

var postgres = string.Format(builder.Configuration["DbConnectionTemplate"]!, host, port, db, user, pass);

builder.Services.AddDbContext<ReferenceDataContext>(options =>
{
    options.UseNpgsql(
        postgres,
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
