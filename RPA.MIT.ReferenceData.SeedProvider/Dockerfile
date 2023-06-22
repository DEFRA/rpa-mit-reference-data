ARG PARENT_VERSION=1.5.0-dotnet6.0

# Development
FROM defradigital/dotnetcore-development:$PARENT_VERSION AS development

ARG PARENT_VERSION
ARG PACKAGE_FEED_URL
ARG PACKAGE_FEED_USERNAME
ARG PACKAGE_FEED_PAT

LABEL uk.gov.defra.parent-image=defra-dotnetcore-development:${PARENT_VERSION}

RUN mkdir -p /home/dotnet/RPA.MIT.ReferenceData.Data/ /home/dotnet/RPA.MIT.ReferenceData.SeedProvider/

COPY --chown=dotnet:dotnet ./docker-nuget.config ./nuget.config

COPY --chown=dotnet:dotnet ./RPA.MIT.ReferenceData.Data/*.csproj ./RPA.MIT.ReferenceData.Data/
RUN dotnet restore ./RPA.MIT.ReferenceData.Data/RPA.MIT.ReferenceData.Data.csproj

COPY --chown=dotnet:dotnet ./RPA.MIT.ReferenceData.SeedProvider/*.csproj ./RPA.MIT.ReferenceData.SeedProvider/
RUN dotnet restore ./RPA.MIT.ReferenceData.SeedProvider/RPA.MIT.ReferenceData.SeedProvider.csproj

COPY --chown=dotnet:dotnet ./RPA.MIT.ReferenceData.SeedProvider/ ./RPA.MIT.ReferenceData.SeedProvider/
COPY --chown=dotnet:dotnet ./RPA.MIT.ReferenceData.Data/ ./RPA.MIT.ReferenceData.Data/

RUN dotnet publish ./RPA.MIT.ReferenceData.SeedProvider/ -c Release -o /home/dotnet/out
