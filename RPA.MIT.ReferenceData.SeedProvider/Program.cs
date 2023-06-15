using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RPA.MIT.ReferenceData.Data;
using RPA.MIT.ReferenceData.SeedProvider.SeedData;

var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

var logger = loggerFactory.CreateLogger<Program>();

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var optionsBuilder = new DbContextOptionsBuilder<ReferenceDataContext>();

optionsBuilder.UseLoggerFactory(loggerFactory);

optionsBuilder.UseNpgsql(
    config["DbConnectionString"],
    x => x.MigrationsAssembly("EST.MIT.ReferenceData.Data")
)
.UseSnakeCaseNamingConvention();

var context = new ReferenceDataContext(optionsBuilder.Options);

SeedProvider.SeedReferenceData(context, logger);
