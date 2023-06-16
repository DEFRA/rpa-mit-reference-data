using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.SeedProvider.SeedData;

var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

var logger = loggerFactory.CreateLogger<Program>();

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .AddJsonFile("appsettings.json")
    .Build();

var host = config["POSTGRES_HOST"];
var db = config["POSTGRES_DB"];
var port = config["POSTGRES_PORT"];
var user = config["POSTGRES_USER"];
var pass = config["POSTGRES_PASSWORD"];

var postgres = string.Format(config["DbConnectionTemplate"]!, host, port, db, user, pass);

var optionsBuilder = new DbContextOptionsBuilder<ReferenceDataContext>();

optionsBuilder.UseLoggerFactory(loggerFactory);

optionsBuilder.UseNpgsql(
    postgres,
    x => x.MigrationsAssembly("EST.MIT.ReferenceData.Data")
)
.UseSnakeCaseNamingConvention();

var context = new ReferenceDataContext(optionsBuilder.Options);

SeedProvider.SeedReferenceData(context, logger);
