﻿// <auto-generated />
using EST.MIT.ReferenceData.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RPA.MIT.ReferenceData.Data;

#nullable disable

namespace EST.MIT.ReferenceData.Data.Migrations
{
    [DbContext(typeof(ReferenceDataContext))]
    partial class ReferenceDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AccountCodeInvoiceRoute", b =>
                {
                    b.Property<int>("AccountCodesCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("account_codes_code_id");

                    b.Property<int>("RoutesRouteId")
                        .HasColumnType("integer")
                        .HasColumnName("routes_route_id");

                    b.HasKey("AccountCodesCodeId", "RoutesRouteId")
                        .HasName("pk_account_code_invoice_route");

                    b.HasIndex("RoutesRouteId")
                        .HasDatabaseName("ix_account_code_invoice_route_routes_route_id");

                    b.ToTable("account_code_invoice_route", (string)null);
                });

            modelBuilder.Entity("DeliveryBodyCodeInvoiceRoute", b =>
                {
                    b.Property<int>("DeliveryBodyCodesCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("delivery_body_codes_code_id");

                    b.Property<int>("RoutesRouteId")
                        .HasColumnType("integer")
                        .HasColumnName("routes_route_id");

                    b.HasKey("DeliveryBodyCodesCodeId", "RoutesRouteId")
                        .HasName("pk_delivery_body_code_invoice_route");

                    b.HasIndex("RoutesRouteId")
                        .HasDatabaseName("ix_delivery_body_code_invoice_route_routes_route_id");

                    b.ToTable("delivery_body_code_invoice_route", (string)null);
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.Codes.AccountCode", b =>
                {
                    b.Property<int>("CodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("code_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CodeId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("RecoveryType")
                        .HasColumnType("text")
                        .HasColumnName("recovery_type");

                    b.HasKey("CodeId")
                        .HasName("pk_account_codes");

                    b.ToTable("account_codes", (string)null);
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.Codes.Combination", b =>
                {
                    b.Property<int>("CombinationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("combination_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CombinationId"));

                    b.Property<int>("AccountCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("account_code_id");

                    b.Property<int>("DeliveryBodyCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("delivery_body_code_id");

                    b.Property<int>("RouteId")
                        .HasColumnType("integer")
                        .HasColumnName("route_id");

                    b.Property<int>("SchemeCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("scheme_code_id");

                    b.HasKey("CombinationId")
                        .HasName("pk_combinations");

                    b.HasIndex("AccountCodeId")
                        .HasDatabaseName("ix_combinations_account_code_id");

                    b.HasIndex("DeliveryBodyCodeId")
                        .HasDatabaseName("ix_combinations_delivery_body_code_id");

                    b.HasIndex("SchemeCodeId")
                        .HasDatabaseName("ix_combinations_scheme_code_id");

                    b.HasIndex("RouteId", "SchemeCodeId", "AccountCodeId", "DeliveryBodyCodeId")
                        .IsUnique()
                        .HasDatabaseName("IX_Combination_Components");

                    b.ToTable("combinations", (string)null);
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.Codes.DeliveryBodyCode", b =>
                {
                    b.Property<int>("CodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("code_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CodeId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("CodeId")
                        .HasName("pk_delivery_body_codes");

                    b.ToTable("delivery_body_codes", (string)null);
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.Codes.FundCode", b =>
                {
                    b.Property<int>("CodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("code_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CodeId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("CodeId")
                        .HasName("pk_fund_codes");

                    b.ToTable("fund_codes", (string)null);
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.Codes.MarketingYearCode", b =>
                {
                    b.Property<int>("CodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("code_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CodeId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("CodeId")
                        .HasName("pk_marketing_year_codes");

                    b.ToTable("marketing_year_codes", (string)null);
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.Codes.SchemeCode", b =>
                {
                    b.Property<int>("CodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("code_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CodeId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("CodeId")
                        .HasName("pk_scheme_codes");

                    b.ToTable("scheme_codes", (string)null);
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.InvoiceRoute", b =>
                {
                    b.Property<int>("RouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("route_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RouteId"));

                    b.Property<int>("InvoiceTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("invoice_type_id");

                    b.Property<int>("OrganisationId")
                        .HasColumnType("integer")
                        .HasColumnName("organisation_id");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("payment_type_id");

                    b.Property<int>("SchemeTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("scheme_type_id");

                    b.HasKey("RouteId")
                        .HasName("pk_invoice_routes");

                    b.HasIndex("OrganisationId")
                        .HasDatabaseName("ix_invoice_routes_organisation_id");

                    b.HasIndex("PaymentTypeId")
                        .HasDatabaseName("ix_invoice_routes_payment_type_id");

                    b.HasIndex("SchemeTypeId")
                        .HasDatabaseName("ix_invoice_routes_scheme_type_id");

                    b.HasIndex("InvoiceTypeId", "OrganisationId", "SchemeTypeId", "PaymentTypeId")
                        .IsUnique()
                        .HasDatabaseName("IX_Route_RouteParameters");

                    b.ToTable("invoice_routes", (string)null);
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.RouteComponents.InvoiceType", b =>
                {
                    b.Property<int>("ComponentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("component_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ComponentId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("ComponentId")
                        .HasName("pk_invoice_types");

                    b.ToTable("invoice_types", (string)null);
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.RouteComponents.Organisation", b =>
                {
                    b.Property<int>("ComponentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("component_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ComponentId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("ComponentId")
                        .HasName("pk_organisations");

                    b.ToTable("organisations", (string)null);
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.RouteComponents.PaymentType", b =>
                {
                    b.Property<int>("ComponentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("component_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ComponentId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("ComponentId")
                        .HasName("pk_payment_types");

                    b.ToTable("payment_types", (string)null);
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.RouteComponents.SchemeType", b =>
                {
                    b.Property<int>("ComponentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("component_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ComponentId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("ComponentId")
                        .HasName("pk_scheme_types");

                    b.ToTable("scheme_types", (string)null);
                });

            modelBuilder.Entity("FundCodeInvoiceRoute", b =>
                {
                    b.Property<int>("FundCodesCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("fund_codes_code_id");

                    b.Property<int>("RoutesRouteId")
                        .HasColumnType("integer")
                        .HasColumnName("routes_route_id");

                    b.HasKey("FundCodesCodeId", "RoutesRouteId")
                        .HasName("pk_fund_code_invoice_route");

                    b.HasIndex("RoutesRouteId")
                        .HasDatabaseName("ix_fund_code_invoice_route_routes_route_id");

                    b.ToTable("fund_code_invoice_route", (string)null);
                });

            modelBuilder.Entity("InvoiceRouteMarketingYearCode", b =>
                {
                    b.Property<int>("MarketingYearCodesCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("marketing_year_codes_code_id");

                    b.Property<int>("RoutesRouteId")
                        .HasColumnType("integer")
                        .HasColumnName("routes_route_id");

                    b.HasKey("MarketingYearCodesCodeId", "RoutesRouteId")
                        .HasName("pk_invoice_route_marketing_year_code");

                    b.HasIndex("RoutesRouteId")
                        .HasDatabaseName("ix_invoice_route_marketing_year_code_routes_route_id");

                    b.ToTable("invoice_route_marketing_year_code", (string)null);
                });

            modelBuilder.Entity("InvoiceRouteSchemeCode", b =>
                {
                    b.Property<int>("RoutesRouteId")
                        .HasColumnType("integer")
                        .HasColumnName("routes_route_id");

                    b.Property<int>("SchemeCodesCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("scheme_codes_code_id");

                    b.HasKey("RoutesRouteId", "SchemeCodesCodeId")
                        .HasName("pk_invoice_route_scheme_code");

                    b.HasIndex("SchemeCodesCodeId")
                        .HasDatabaseName("ix_invoice_route_scheme_code_scheme_codes_code_id");

                    b.ToTable("invoice_route_scheme_code", (string)null);
                });

            modelBuilder.Entity("AccountCodeInvoiceRoute", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.AccountCode", null)
                        .WithMany()
                        .HasForeignKey("AccountCodesCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_account_code_invoice_route_account_codes_account_codes_code");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.InvoiceRoute", null)
                        .WithMany()
                        .HasForeignKey("RoutesRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_account_code_invoice_route_invoice_routes_routes_route_id");
                });

            modelBuilder.Entity("DeliveryBodyCodeInvoiceRoute", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.DeliveryBodyCode", null)
                        .WithMany()
                        .HasForeignKey("DeliveryBodyCodesCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_delivery_body_code_invoice_route_delivery_body_codes_delive");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.InvoiceRoute", null)
                        .WithMany()
                        .HasForeignKey("RoutesRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_delivery_body_code_invoice_route_invoice_routes_routes_rout");
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.Codes.Combination", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.AccountCode", "AccountCode")
                        .WithMany()
                        .HasForeignKey("AccountCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_combinations_account_codes_account_code_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.DeliveryBodyCode", "DeliveryBodyCode")
                        .WithMany()
                        .HasForeignKey("DeliveryBodyCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_combinations_delivery_body_codes_delivery_body_code_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.InvoiceRoute", "Route")
                        .WithMany("Combinations")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_combinations_invoice_routes_route_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.SchemeCode", "SchemeCode")
                        .WithMany()
                        .HasForeignKey("SchemeCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_combinations_scheme_codes_scheme_code_id");

                    b.Navigation("AccountCode");

                    b.Navigation("DeliveryBodyCode");

                    b.Navigation("Route");

                    b.Navigation("SchemeCode");
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.InvoiceRoute", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.RouteComponents.InvoiceType", "InvoiceType")
                        .WithMany()
                        .HasForeignKey("InvoiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_invoice_routes_invoice_types_invoice_type_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.RouteComponents.Organisation", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_invoice_routes_organisations_organisation_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.RouteComponents.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_invoice_routes_payment_types_payment_type_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.RouteComponents.SchemeType", "SchemeType")
                        .WithMany()
                        .HasForeignKey("SchemeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_invoice_routes_scheme_types_scheme_type_id");

                    b.Navigation("InvoiceType");

                    b.Navigation("Organisation");

                    b.Navigation("PaymentType");

                    b.Navigation("SchemeType");
                });

            modelBuilder.Entity("FundCodeInvoiceRoute", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.FundCode", null)
                        .WithMany()
                        .HasForeignKey("FundCodesCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_fund_code_invoice_route_fund_codes_fund_codes_code_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.InvoiceRoute", null)
                        .WithMany()
                        .HasForeignKey("RoutesRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_fund_code_invoice_route_invoice_routes_routes_route_id");
                });

            modelBuilder.Entity("InvoiceRouteMarketingYearCode", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.MarketingYearCode", null)
                        .WithMany()
                        .HasForeignKey("MarketingYearCodesCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_invoice_route_marketing_year_code_marketing_year_codes_mark");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.InvoiceRoute", null)
                        .WithMany()
                        .HasForeignKey("RoutesRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_invoice_route_marketing_year_code_invoice_routes_routes_rou");
                });

            modelBuilder.Entity("InvoiceRouteSchemeCode", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.InvoiceRoute", null)
                        .WithMany()
                        .HasForeignKey("RoutesRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_invoice_route_scheme_code_invoice_routes_routes_route_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.SchemeCode", null)
                        .WithMany()
                        .HasForeignKey("SchemeCodesCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_invoice_route_scheme_code_scheme_codes_scheme_codes_code_id");
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.InvoiceRoute", b =>
                {
                    b.Navigation("Combinations");
                });
#pragma warning restore 612, 618
        }
    }
}
