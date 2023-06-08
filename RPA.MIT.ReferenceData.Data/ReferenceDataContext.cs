using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RPA.MIT.ReferenceData.Data.Models;
using RPA.MIT.ReferenceData.Data.Models.Codes;
using RPA.MIT.ReferenceData.Data.Models.RouteComponents;

namespace RPA.MIT.ReferenceData.Data;

[ExcludeFromCodeCoverage]
public class ReferenceDataContext : DbContext
{
    // Route Components
    public DbSet<InvoiceRoute> InvoiceRoutes => Set<InvoiceRoute>();

    public DbSet<InvoiceType> InvoiceTypes => Set<InvoiceType>();

    public DbSet<Organisation> Organisations => Set<Organisation>();

    public DbSet<SchemeType> SchemeTypes => Set<SchemeType>();

    public DbSet<PaymentType> PaymentTypes => Set<PaymentType>();

    // Codes
    public DbSet<SchemeCode> SchemeCodes => Set<SchemeCode>();

    public DbSet<AccountCode> AccountCodes => Set<AccountCode>();

    public DbSet<FundCode> FundCodes => Set<FundCode>();

    public DbSet<DeliveryBodyCode> DeliveryBodyCodes => Set<DeliveryBodyCode>();

    public DbSet<MarketingYearCode> MarketingYearCodes => Set<MarketingYearCode>();

    public DbSet<Combination> Combinations => Set<Combination>();

    public ReferenceDataContext(DbContextOptions<ReferenceDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InvoiceRoute>()
            .HasIndex(r => new { r.InvoiceTypeId, DeliveryBodyId = r.OrganisationId, r.SchemeTypeId, r.PaymentTypeId })
            .HasDatabaseName("IX_Route_RouteParameters")
            .IsUnique();

        modelBuilder.Entity<Combination>()
            .HasIndex(c => new { c.RouteId, c.SchemeCodeId, c.AccountCodeId, c.DeliveryBodyCodeId })
            .HasDatabaseName("IX_Combination_Components")
            .IsUnique();
    }
}
