using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EST.MIT.ReferenceData.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeliveryBodyToOrganisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_invoice_routes_delivery_bodies_delivery_body_id",
                table: "invoice_routes");

            migrationBuilder.DropTable(
                name: "delivery_bodies");

            migrationBuilder.RenameColumn(
                name: "delivery_body_id",
                table: "invoice_routes",
                newName: "organisation_id");

            migrationBuilder.RenameIndex(
                name: "ix_invoice_routes_delivery_body_id",
                table: "invoice_routes",
                newName: "ix_invoice_routes_organisation_id");

            migrationBuilder.CreateTable(
                name: "organisations",
                columns: table => new
                {
                    component_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organisations", x => x.component_id);
                });

            migrationBuilder.AddForeignKey(
                name: "fk_invoice_routes_organisations_organisation_id",
                table: "invoice_routes",
                column: "organisation_id",
                principalTable: "organisations",
                principalColumn: "component_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_invoice_routes_organisations_organisation_id",
                table: "invoice_routes");

            migrationBuilder.DropTable(
                name: "organisations");

            migrationBuilder.RenameColumn(
                name: "organisation_id",
                table: "invoice_routes",
                newName: "delivery_body_id");

            migrationBuilder.RenameIndex(
                name: "ix_invoice_routes_organisation_id",
                table: "invoice_routes",
                newName: "ix_invoice_routes_delivery_body_id");

            migrationBuilder.CreateTable(
                name: "delivery_bodies",
                columns: table => new
                {
                    component_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_delivery_bodies", x => x.component_id);
                });

            migrationBuilder.AddForeignKey(
                name: "fk_invoice_routes_delivery_bodies_delivery_body_id",
                table: "invoice_routes",
                column: "delivery_body_id",
                principalTable: "delivery_bodies",
                principalColumn: "component_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
