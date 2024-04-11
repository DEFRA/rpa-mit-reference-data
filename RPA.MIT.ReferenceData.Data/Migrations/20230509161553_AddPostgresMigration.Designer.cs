﻿// <auto-generated />
using EST.MIT.ReferenceData.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RPA.MIT.ReferenceData.Data;

#nullable disable

namespace EST.MIT.ReferenceData.Data.Migrations
{
    [DbContext(typeof(ReferenceDataContext))]
    [Migration("20230509161553_AddPostgresMigration")]
    partial class AddPostgresMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AccountCodeRoute", b =>
                {
                    b.Property<int>("AccountCodesCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("account_codes_code_id");

                    b.Property<int>("RoutesRouteId")
                        .HasColumnType("integer")
                        .HasColumnName("routes_route_id");

                    b.HasKey("AccountCodesCodeId", "RoutesRouteId")
                        .HasName("pk_account_code_route");

                    b.HasIndex("RoutesRouteId")
                        .HasDatabaseName("ix_account_code_route_routes_route_id");

                    b.ToTable("account_code_route", (string)null);
                });

            modelBuilder.Entity("DeliveryBodyCodeRoute", b =>
                {
                    b.Property<int>("DeliveryBodyCodesCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("delivery_body_codes_code_id");

                    b.Property<int>("RoutesRouteId")
                        .HasColumnType("integer")
                        .HasColumnName("routes_route_id");

                    b.HasKey("DeliveryBodyCodesCodeId", "RoutesRouteId")
                        .HasName("pk_delivery_body_code_route");

                    b.HasIndex("RoutesRouteId")
                        .HasDatabaseName("ix_delivery_body_code_route_routes_route_id");

                    b.ToTable("delivery_body_code_route", (string)null);
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

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.Route", b =>
                {
                    b.Property<int>("RouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("route_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RouteId"));

                    b.Property<int>("DeliveryBodyId")
                        .HasColumnType("integer")
                        .HasColumnName("delivery_body_id");

                    b.Property<int>("InvoiceTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("invoice_type_id");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("payment_type_id");

                    b.Property<int>("SchemeTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("scheme_type_id");

                    b.HasKey("RouteId")
                        .HasName("pk_routes");

                    b.HasIndex("DeliveryBodyId")
                        .HasDatabaseName("ix_routes_delivery_body_id");

                    b.HasIndex("PaymentTypeId")
                        .HasDatabaseName("ix_routes_payment_type_id");

                    b.HasIndex("SchemeTypeId")
                        .HasDatabaseName("ix_routes_scheme_type_id");

                    b.HasIndex("InvoiceTypeId", "DeliveryBodyId", "SchemeTypeId", "PaymentTypeId")
                        .IsUnique()
                        .HasDatabaseName("IX_Route_RouteParameters");

                    b.ToTable("routes", (string)null);
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.RouteComponents.DeliveryBody", b =>
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
                        .HasName("pk_delivery_bodies");

                    b.ToTable("delivery_bodies", (string)null);
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

            modelBuilder.Entity("FundCodeRoute", b =>
                {
                    b.Property<int>("FundCodesCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("fund_codes_code_id");

                    b.Property<int>("RoutesRouteId")
                        .HasColumnType("integer")
                        .HasColumnName("routes_route_id");

                    b.HasKey("FundCodesCodeId", "RoutesRouteId")
                        .HasName("pk_fund_code_route");

                    b.HasIndex("RoutesRouteId")
                        .HasDatabaseName("ix_fund_code_route_routes_route_id");

                    b.ToTable("fund_code_route", (string)null);
                });

            modelBuilder.Entity("MarketingYearCodeRoute", b =>
                {
                    b.Property<int>("MarketingYearCodesCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("marketing_year_codes_code_id");

                    b.Property<int>("RoutesRouteId")
                        .HasColumnType("integer")
                        .HasColumnName("routes_route_id");

                    b.HasKey("MarketingYearCodesCodeId", "RoutesRouteId")
                        .HasName("pk_marketing_year_code_route");

                    b.HasIndex("RoutesRouteId")
                        .HasDatabaseName("ix_marketing_year_code_route_routes_route_id");

                    b.ToTable("marketing_year_code_route", (string)null);
                });

            modelBuilder.Entity("RouteSchemeCode", b =>
                {
                    b.Property<int>("RoutesRouteId")
                        .HasColumnType("integer")
                        .HasColumnName("routes_route_id");

                    b.Property<int>("SchemeCodesCodeId")
                        .HasColumnType("integer")
                        .HasColumnName("scheme_codes_code_id");

                    b.HasKey("RoutesRouteId", "SchemeCodesCodeId")
                        .HasName("pk_route_scheme_code");

                    b.HasIndex("SchemeCodesCodeId")
                        .HasDatabaseName("ix_route_scheme_code_scheme_codes_code_id");

                    b.ToTable("route_scheme_code", (string)null);
                });

            modelBuilder.Entity("AccountCodeRoute", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.AccountCode", null)
                        .WithMany()
                        .HasForeignKey("AccountCodesCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_account_code_route_account_codes_account_codes_code_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Route", null)
                        .WithMany()
                        .HasForeignKey("RoutesRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_account_code_route_routes_routes_route_id");
                });

            modelBuilder.Entity("DeliveryBodyCodeRoute", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.DeliveryBodyCode", null)
                        .WithMany()
                        .HasForeignKey("DeliveryBodyCodesCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_delivery_body_code_route_delivery_body_codes_delivery_body_");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Route", null)
                        .WithMany()
                        .HasForeignKey("RoutesRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_delivery_body_code_route_routes_routes_route_id");
                });

            modelBuilder.Entity("EST.MIT.ReferenceData.Data.Models.Route", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.RouteComponents.DeliveryBody", "DeliveryBody")
                        .WithMany()
                        .HasForeignKey("DeliveryBodyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_routes_delivery_bodies_delivery_body_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.RouteComponents.InvoiceType", "InvoiceType")
                        .WithMany()
                        .HasForeignKey("InvoiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_routes_invoice_types_invoice_type_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.RouteComponents.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_routes_payment_types_payment_type_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.RouteComponents.SchemeType", "SchemeType")
                        .WithMany()
                        .HasForeignKey("SchemeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_routes_scheme_types_scheme_type_id");

                    b.Navigation("DeliveryBody");

                    b.Navigation("InvoiceType");

                    b.Navigation("PaymentType");

                    b.Navigation("SchemeType");
                });

            modelBuilder.Entity("FundCodeRoute", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.FundCode", null)
                        .WithMany()
                        .HasForeignKey("FundCodesCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_fund_code_route_fund_codes_fund_codes_code_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Route", null)
                        .WithMany()
                        .HasForeignKey("RoutesRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_fund_code_route_routes_routes_route_id");
                });

            modelBuilder.Entity("MarketingYearCodeRoute", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.MarketingYearCode", null)
                        .WithMany()
                        .HasForeignKey("MarketingYearCodesCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_marketing_year_code_route_marketing_year_codes_marketing_ye");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Route", null)
                        .WithMany()
                        .HasForeignKey("RoutesRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_marketing_year_code_route_routes_routes_route_id");
                });

            modelBuilder.Entity("RouteSchemeCode", b =>
                {
                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Route", null)
                        .WithMany()
                        .HasForeignKey("RoutesRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_route_scheme_code_routes_routes_route_id");

                    b.HasOne("EST.MIT.ReferenceData.Data.Models.Codes.SchemeCode", null)
                        .WithMany()
                        .HasForeignKey("SchemeCodesCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_route_scheme_code_scheme_codes_scheme_codes_code_id");
                });
#pragma warning restore 612, 618
        }
    }
}
