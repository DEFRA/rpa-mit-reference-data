# rpa-mit-reference-data

This repository hosts a minimal API developed using .NET 8, designed to supply invoice template reference data that ensures that only valid manual invoices can be produced.

The api is called from other MIT components when scheme data is needed, for example a scheme code could be passed in to the api and a list of valid options for that code are returned.

## Requirements

-  .NET 8 SDK - amend as needed for your distro
```bash
wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update && sudo apt-get install -y dotnet-sdk-8.0
```
-  EF Core Tools
```bash
dotnet tool install --global dotnet-ef
```
-  PostgreSQL database
-  [Docker](https://docs.docker.com/engine/install/debian/) - amend as needed for your distro
---
## Create the database

Create the postgres database in docker

```bash
docker pull postgres
```
```bash
docker run --name MY_POSTGRES_DB -e POSTGRES_PASSWORD=password -p 5432:5432 -d postgres
```

---
## Local Setup

To run this service locally complete the following steps.
### Set up user secrets

Use the secrets-template to create a secrets.json in the same folder location as the [RPA.MIT.ReferenceData.Api.csproj](https://github.com/DEFRA/rpa-mit-reference-data/blob/main/RPA.MIT.ReferenceData.Api/RPA.MIT.ReferenceData.Api.csproj "RPA.MIT.ReferenceData.Api.csproj") file. 

Some **example** values might be

```json
{
    "POSTGRES_HOST": "local",
    "POSTGRES_PORT": "5432",
    "POSTGRES_DB": "my db",
    "POSTGRES_USER": "username",
    "POSTGRES_PASSWORD": "password",
    "AzureADPostgreSQLResourceID": "https://ossrdbms-aad.database.windows.net/.default",
    "DbConnectionTemplate": "Server={0};Port={1};Database={2};User Id={3};Password={4};"
}
```

Once this is done run the following command to add the projects user secrets

```bash
cat secrets.json | dotnet user-secrets set
```

These values can also be created as environment variables or as a development app settings file.

### Apply DB migrations

We use EF Core to handle database migrations. Run the following command to update migrations on database.

**NOTE** - You will need to create the database in postgres before migrating.

```bash
dotnet ef database update
```

### Start the Api

```bash
cd RPA.MIT.ReferenceData.Api
```
```bash
dotnet run
```


---
## Running in Docker

To create the application as a docker container run the following command in the parent directory.

```bash
docker compose up
```

---
## Endpoints

API Endpoints are avaliable from [https://localhost:7012](https://localhost:7012).
Swagger is also available in developement environments from [https://localhost:7012/swagger](https://localhost:7012/swagger)
