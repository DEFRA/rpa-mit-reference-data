using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace EST.MIT.ReferenceData.Data.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class AddCombinationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "combinations",
                columns: table => new
                {
                    combination_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    route_id = table.Column<int>(type: "integer", nullable: false),
                    scheme_code_id = table.Column<int>(type: "integer", nullable: false),
                    account_code_id = table.Column<int>(type: "integer", nullable: false),
                    delivery_body_code_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_combinations", x => x.combination_id);
                    table.ForeignKey(
                        name: "fk_combinations_account_codes_account_code_id",
                        column: x => x.account_code_id,
                        principalTable: "account_codes",
                        principalColumn: "code_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_combinations_delivery_body_codes_delivery_body_code_id",
                        column: x => x.delivery_body_code_id,
                        principalTable: "delivery_body_codes",
                        principalColumn: "code_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_combinations_routes_route_id",
                        column: x => x.route_id,
                        principalTable: "routes",
                        principalColumn: "route_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_combinations_scheme_codes_scheme_code_id",
                        column: x => x.scheme_code_id,
                        principalTable: "scheme_codes",
                        principalColumn: "code_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Combination_Components",
                table: "combinations",
                columns: new[] { "route_id", "scheme_code_id", "account_code_id", "delivery_body_code_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_combinations_account_code_id",
                table: "combinations",
                column: "account_code_id");

            migrationBuilder.CreateIndex(
                name: "ix_combinations_delivery_body_code_id",
                table: "combinations",
                column: "delivery_body_code_id");

            migrationBuilder.CreateIndex(
                name: "ix_combinations_scheme_code_id",
                table: "combinations",
                column: "scheme_code_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "combinations");
        }
    }
}
