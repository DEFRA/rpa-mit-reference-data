using EST.MIT.ReferenceData.Api.Exceptions;
using EST.MIT.ReferenceData.Data;
using EST.MIT.ReferenceData.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.ReferenceData.Api;

/// <summary>
/// Extension methods to provide filters for invoice routes.
/// </summary>
public static class InvoiceRouteFilters
{
    /// <summary>
    /// Filter invoice routes by invoice type.
    /// </summary>
    /// <param name="query"><see cref="InvoiceRoute" /> query</param>
    /// <param name="context">ReferenceData DbContext</param>
    /// <param name="invoiceType">Invoice type filter</param>
    /// <returns>Queryable <see cref="InvoiceRoute" /> with added filter</returns>
    /// <exception cref="InvoiceTypeNotFoundException">Thrown if the invoice type is not present in DbContext.</exception>
    public async static Task<IQueryable<InvoiceRoute>> FilterByInvoiceType(this IQueryable<InvoiceRoute> query, ReferenceDataContext context, string invoiceType)
    {
        try
        {
            await context.InvoiceTypes.SingleAsync(x => x.Code == invoiceType);
        }
        catch (InvalidOperationException e)
        {
            throw new InvoiceTypeNotFoundException($"Invoice type ({invoiceType}) code not found in reference data.", e);
        }

        return query.Where(x => x.InvoiceType.Code == invoiceType);
    }

    /// <summary>
    /// Filter invoice routes by organisation type.
    /// </summary>
    /// <param name="query"><see cref="InvoiceRoute" /> query</param>
    /// <param name="context">ReferenceData DbContext</param>
    /// <param name="organisation">Organisation type filter</param>
    /// <returns>Queryable <see cref="InvoiceRoute" /> with added filter</returns>
    /// <exception cref="DeliveryBodyNotFoundException">Thrown if the delivery body is not present in DbContext.</exception>
    public async static Task<IQueryable<InvoiceRoute>> FilterByOrganisation(this IQueryable<InvoiceRoute> query, ReferenceDataContext context, string organisation)
    {
        try
        {
            await context.Organisations.SingleAsync(x => x.Code == organisation);
        }
        catch (InvalidOperationException e)
        {
            throw new DeliveryBodyNotFoundException($"Delivery body ({organisation}) code not found in reference data.", e);
        }

        return query.Where(x => x.Organisation.Code == organisation);
    }

    /// <summary>
    /// Filter invoice routes by scheme type type.
    /// </summary>
    /// <param name="query">Invoice route query</param>
    /// <param name="context">ReferenceData DbContext</param>
    /// <param name="schemeType">Scheme type filter</param>
    /// <returns>Queryable <see cref="InvoiceRoute" /> with added filter</returns>
    /// <exception cref="SchemeTypeNotFoundException">Thrown if the scheme type is not present in DbContext.</exception>
    public async static Task<IQueryable<InvoiceRoute>> FilterBySchemeType(this IQueryable<InvoiceRoute> query, ReferenceDataContext context, string schemeType)
    {
        try
        {
            await context.SchemeTypes.SingleAsync(x => x.Code == schemeType);
        }
        catch (InvalidOperationException e)
        {
            throw new SchemeTypeNotFoundException($"Scheme type ({schemeType}) code not found in reference data.", e);
        }

        return query.Where(x => x.SchemeType.Code == schemeType);
    }
}
