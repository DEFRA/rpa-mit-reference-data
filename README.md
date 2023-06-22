# RPA-MIT-ReferenceData
A minimal api for supplying invoice template reference data (.NET 6)

## Running Application
### Requirements
* Git
* .NET 6 SDK
* PostgreSQL
* **Optional:** Docker - Only needed if running PostgreSQL within container

### PostgreSQL
Execute the following commands to run Postgres inside a docker container:
```ps
docker pull postgres
docker run --name some-postgres -e POSTGRES_PASSWORD=mysecretpassword -d postgres
```

Or install a standalone instance using the following link:

[PostgreSQL: Windows installers](https://www.postgresql.org/download/windows/)

### EF Core Tools
Follow this guide to install EF Core global tools:

[Entity Framework Core tools reference - .NET Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### Environment Variables
The following environment variables are required by the application container.

| Name                	| Default  	|
|---------------------	|----------	|
| POSTGRES_HOST 	    |          	|
| POSTGRES_USER       	| postgres 	|
| POSTGRES_PASS       	| password 	|

### Add Private Package Feed
This project uses a private NuGet package to store seed data for the SeedProvider project.

Follow this guide to add the private feed to Visual Studio:

[Install NuGet packages with Visual Studio](https://learn.microsoft.com/en-us/azure/devops/artifacts/nuget/consume?view=azure-devops&tabs=windows)

### Setup Database
This project uses EF Core to handle database migrations. Run the following command to update migrations on database.

```ps
dotnet ef database update --project .\RPA.MIT.ReferenceData.Api
```

### Seeding Reference Data
**Important**: The seed ref data provider will reset the connected database to reference data defaults.

The seed provider uses dotnet user secrets to store Postgres connection parameters.

A separate project has been created to provide seed data which can be ran using:
```cs
cd RPA.MIT.ReferenceData.SeedProvider
dotnet run
```

This application dynamically seeds reference data into the database based on Json files from the `RPA.MIT.Reference` package.

#### Export Reference Data
You can export the seed data to SQL scripts by using `docker-compose.seed.yaml`.

```ps
docker compose -f docker-compose.seed.yaml
```

The seed operation has completed when the following log entry is displayed:
```log
info: Program[0]
      Seeding reference data completed in 14 seconds
```

Once the logs show that the seed operation has completed, run the following command to dump tables:
```ps
docker exec -it rpa-mit-referencedata-rpa-mit-reference-data-postgres-1 sh /home/postgres/extract-seed-data.sh
```

This will store the SQL INSERT scripts for each table in the `mit_reference_data` database in `{SOLUTION_DIR}/seed-data-scripts`.

### Starting Api
To start the Api without seeding reference data use the following:
```ps
cd RPA.MIT.ReferenceData.Api
dotnet run
```

To seed reference data before start up, use the following variation:
```ps
cd RPA.MIT.ReferenceData.Api
dotnet run --seed-ref-data
```

Endpoints are accessible at https://localhost:7012.

Swagger page is accessible at https://localhost:7012/swagger/index.html.
