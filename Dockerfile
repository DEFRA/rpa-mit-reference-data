ARG PARENT_VERSION=1.5.0-dotnet6.0

# Development
FROM defradigital/dotnetcore-development:$PARENT_VERSION AS development

ARG PARENT_VERSION

LABEL uk.gov.defra.parent-image=defra-dotnetcore-development:${PARENT_VERSION}

RUN mkdir -p /home/dotnet/RPA.MIT.ReferenceData.Data/ /home/dotnet/RPA.MIT.ReferenceData.Api/ /home/dotnet/RPA.MIT.ReferenceData.Api.Test/

COPY --chown=dotnet:dotnet ./RPA.MIT.ReferenceData.Data/*.csproj ./RPA.MIT.ReferenceData.Data/
RUN dotnet restore ./RPA.MIT.ReferenceData.Data/RPA.MIT.ReferenceData.Data.csproj

COPY --chown=dotnet:dotnet ./RPA.MIT.ReferenceData.Api/*.csproj ./RPA.MIT.ReferenceData.Api/
RUN dotnet restore ./RPA.MIT.ReferenceData.Api/RPA.MIT.ReferenceData.Api.csproj

COPY --chown=dotnet:dotnet ./RPA.MIT.ReferenceData.Api.Test/*.csproj ./RPA.MIT.ReferenceData.Api.Test/
RUN dotnet restore ./RPA.MIT.ReferenceData.Api.Test/RPA.MIT.ReferenceData.Api.Test.csproj

COPY --chown=dotnet:dotnet ./RPA.MIT.ReferenceData.Api/ ./RPA.MIT.ReferenceData.Api/
COPY --chown=dotnet:dotnet ./RPA.MIT.ReferenceData.Data/ ./RPA.MIT.ReferenceData.Data/
COPY --chown=dotnet:dotnet ./RPA.MIT.ReferenceData.Api.Test/ ./RPA.MIT.ReferenceData.Api.Test/

RUN dotnet publish ./RPA.MIT.ReferenceData.Api/ -c Release -o /home/dotnet/out

ARG PORT=3000
ENV PORT ${PORT}
EXPOSE ${PORT}

CMD dotnet watch --project ./RPA.MIT.ReferenceData.Api run --urls "http://*:${PORT}"

# Production
FROM defradigital/dotnetcore:$PARENT_VERSION AS production

ARG PARENT_VERSION
ARG PARENT_REGISTRY

LABEL uk.gov.defra.parent-image=defra-dotnetcore-development:${PARENT_VERSION}

ARG PORT=3000
ENV ASPNETCORE_URLS=http://*:${PORT}
EXPOSE ${PORT}

COPY --from=development /home/dotnet/out/ ./

CMD dotnet RPA.MIT.ReferenceData.Api.dll