using System.Diagnostics.CodeAnalysis;
using EST.MIT.ReferenceData.Data;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EST.MIT.ReferenceData.Api.Models.ParameterFilters;

#pragma warning disable CS1591

[ExcludeFromCodeCoverage]
public class OrganisationParameterFilter : IParameterFilter
{
    private readonly IServiceScopeFactory _scopeFactory;

    public OrganisationParameterFilter(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        if (parameter.Name.Equals("Organisation", StringComparison.InvariantCultureIgnoreCase))
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

            parameter.Schema.Enum = db.Organisations.Select(x => new OpenApiString(x.Code)).ToList<IOpenApiAny>();
        }
    }
}

#pragma warning restore CS1591
