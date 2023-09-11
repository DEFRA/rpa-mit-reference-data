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
The following environment variables are required by the application.

| Name              	| Description                         	| Default                         	|
|-------------------	|-------------------------------------	|---------------------------------	|
| POSTGRES_HOST     	| Hostname of the Postgres server     	| rpa-mit-reference-data-postgres 	|
| POSTGRES_DB       	| Name of the reference data database 	| rpa_mit_reference_data          	|
| POSTGRES_USER     	| Postgres username                   	| postgres                        	|
| POSTGRES_PASSWORD 	| Postgres password                   	| password                        	|
| POSTGRES_PORT     	| Postgres server port                	| 5432                            	|
| SCHEMA_DEFAULT    	| Default schema name                 	| public                          	|

When running using Docker / Docker Compose these values are populated from environment variables.

If running locally using `dotnet run` the values are populated from dotnet user-secrets. Please see [Safe storage of app secrets in development in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows)

### Setup Database
This project uses EF Core to handle database migrations. Run the following command to update migrations on database.

```ps
dotnet ef database update --project .\RPA.MIT.ReferenceData.Api
```

### Starting Api
```ps
cd RPA.MIT.ReferenceData.Api
dotnet run
```

Endpoints are accessible at https://localhost:7012.

Swagger page is accessible at https://localhost:7012/swagger/index.html.
test