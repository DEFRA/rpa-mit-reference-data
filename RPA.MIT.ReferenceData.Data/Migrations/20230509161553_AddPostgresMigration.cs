using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace EST.MIT.ReferenceData.Data.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class AddPostgresMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account_codes",
                columns: table => new
                {
                    code_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recovery_type = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account_codes", x => x.code_id);
                });

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

            migrationBuilder.CreateTable(
                name: "delivery_body_codes",
                columns: table => new
                {
                    code_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_delivery_body_codes", x => x.code_id);
                });

            migrationBuilder.CreateTable(
                name: "fund_codes",
                columns: table => new
                {
                    code_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fund_codes", x => x.code_id);
                });

            migrationBuilder.CreateTable(
                name: "invoice_types",
                columns: table => new
                {
                    component_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_invoice_types", x => x.component_id);
                });

            migrationBuilder.CreateTable(
                name: "marketing_year_codes",
                columns: table => new
                {
                    code_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_marketing_year_codes", x => x.code_id);
                });

            migrationBuilder.CreateTable(
                name: "payment_types",
                columns: table => new
                {
                    component_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_types", x => x.component_id);
                });

            migrationBuilder.CreateTable(
                name: "scheme_codes",
                columns: table => new
                {
                    code_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scheme_codes", x => x.code_id);
                });

            migrationBuilder.CreateTable(
                name: "scheme_types",
                columns: table => new
                {
                    component_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scheme_types", x => x.component_id);
                });

            migrationBuilder.CreateTable(
                name: "routes",
                columns: table => new
                {
                    route_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    invoice_type_id = table.Column<int>(type: "integer", nullable: false),
                    delivery_body_id = table.Column<int>(type: "integer", nullable: false),
                    scheme_type_id = table.Column<int>(type: "integer", nullable: false),
                    payment_type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_routes", x => x.route_id);
                    table.ForeignKey(
                        name: "fk_routes_delivery_bodies_delivery_body_id",
                        column: x => x.delivery_body_id,
                        principalTable: "delivery_bodies",
                        principalColumn: "component_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_routes_invoice_types_invoice_type_id",
                        column: x => x.invoice_type_id,
                        principalTable: "invoice_types",
                        principalColumn: "component_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_routes_payment_types_payment_type_id",
                        column: x => x.payment_type_id,
                        principalTable: "payment_types",
                        principalColumn: "component_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_routes_scheme_types_scheme_type_id",
                        column: x => x.scheme_type_id,
                        principalTable: "scheme_types",
                        principalColumn: "component_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "account_code_route",
                columns: table => new
                {
                    account_codes_code_id = table.Column<int>(type: "integer", nullable: false),
                    routes_route_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account_code_route", x => new { x.account_codes_code_id, x.routes_route_id });
                    table.ForeignKey(
                        name: "fk_account_code_route_account_codes_account_codes_code_id",
                        column: x => x.account_codes_code_id,
                        principalTable: "account_codes",
                        principalColumn: "code_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_account_code_route_routes_routes_route_id",
                        column: x => x.routes_route_id,
                        principalTable: "routes",
                        principalColumn: "route_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "delivery_body_code_route",
                columns: table => new
                {
                    delivery_body_codes_code_id = table.Column<int>(type: "integer", nullable: false),
                    routes_route_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_delivery_body_code_route", x => new { x.delivery_body_codes_code_id, x.routes_route_id });
                    table.ForeignKey(
                        name: "fk_delivery_body_code_route_delivery_body_codes_delivery_body_",
                        column: x => x.delivery_body_codes_code_id,
                        principalTable: "delivery_body_codes",
                        principalColumn: "code_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_delivery_body_code_route_routes_routes_route_id",
                        column: x => x.routes_route_id,
                        principalTable: "routes",
                        principalColumn: "route_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fund_code_route",
                columns: table => new
                {
                    fund_codes_code_id = table.Column<int>(type: "integer", nullable: false),
                    routes_route_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fund_code_route", x => new { x.fund_codes_code_id, x.routes_route_id });
                    table.ForeignKey(
                        name: "fk_fund_code_route_fund_codes_fund_codes_code_id",
                        column: x => x.fund_codes_code_id,
                        principalTable: "fund_codes",
                        principalColumn: "code_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_fund_code_route_routes_routes_route_id",
                        column: x => x.routes_route_id,
                        principalTable: "routes",
                        principalColumn: "route_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "marketing_year_code_route",
                columns: table => new
                {
                    marketing_year_codes_code_id = table.Column<int>(type: "integer", nullable: false),
                    routes_route_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_marketing_year_code_route", x => new { x.marketing_year_codes_code_id, x.routes_route_id });
                    table.ForeignKey(
                        name: "fk_marketing_year_code_route_marketing_year_codes_marketing_ye",
                        column: x => x.marketing_year_codes_code_id,
                        principalTable: "marketing_year_codes",
                        principalColumn: "code_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_marketing_year_code_route_routes_routes_route_id",
                        column: x => x.routes_route_id,
                        principalTable: "routes",
                        principalColumn: "route_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "route_scheme_code",
                columns: table => new
                {
                    routes_route_id = table.Column<int>(type: "integer", nullable: false),
                    scheme_codes_code_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_route_scheme_code", x => new { x.routes_route_id, x.scheme_codes_code_id });
                    table.ForeignKey(
                        name: "fk_route_scheme_code_routes_routes_route_id",
                        column: x => x.routes_route_id,
                        principalTable: "routes",
                        principalColumn: "route_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_route_scheme_code_scheme_codes_scheme_codes_code_id",
                        column: x => x.scheme_codes_code_id,
                        principalTable: "scheme_codes",
                        principalColumn: "code_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_account_code_route_routes_route_id",
                table: "account_code_route",
                column: "routes_route_id");

            migrationBuilder.CreateIndex(
                name: "ix_delivery_body_code_route_routes_route_id",
                table: "delivery_body_code_route",
                column: "routes_route_id");

            migrationBuilder.CreateIndex(
                name: "ix_fund_code_route_routes_route_id",
                table: "fund_code_route",
                column: "routes_route_id");

            migrationBuilder.CreateIndex(
                name: "ix_marketing_year_code_route_routes_route_id",
                table: "marketing_year_code_route",
                column: "routes_route_id");

            migrationBuilder.CreateIndex(
                name: "ix_route_scheme_code_scheme_codes_code_id",
                table: "route_scheme_code",
                column: "scheme_codes_code_id");

            migrationBuilder.CreateIndex(
                name: "IX_Route_RouteParameters",
                table: "routes",
                columns: new[] { "invoice_type_id", "delivery_body_id", "scheme_type_id", "payment_type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_routes_delivery_body_id",
                table: "routes",
                column: "delivery_body_id");

            migrationBuilder.CreateIndex(
                name: "ix_routes_payment_type_id",
                table: "routes",
                column: "payment_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_routes_scheme_type_id",
                table: "routes",
                column: "scheme_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_code_route");

            migrationBuilder.DropTable(
                name: "delivery_body_code_route");

            migrationBuilder.DropTable(
                name: "fund_code_route");

            migrationBuilder.DropTable(
                name: "marketing_year_code_route");

            migrationBuilder.DropTable(
                name: "route_scheme_code");

            migrationBuilder.DropTable(
                name: "account_codes");

            migrationBuilder.DropTable(
                name: "delivery_body_codes");

            migrationBuilder.DropTable(
                name: "fund_codes");

            migrationBuilder.DropTable(
                name: "marketing_year_codes");

            migrationBuilder.DropTable(
                name: "routes");

            migrationBuilder.DropTable(
                name: "scheme_codes");

            migrationBuilder.DropTable(
                name: "delivery_bodies");

            migrationBuilder.DropTable(
                name: "invoice_types");

            migrationBuilder.DropTable(
                name: "payment_types");

            migrationBuilder.DropTable(
                name: "scheme_types");
        }
    }
}
