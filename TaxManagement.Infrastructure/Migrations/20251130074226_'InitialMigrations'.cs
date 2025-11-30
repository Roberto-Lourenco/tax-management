using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "tax_rules",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    origin_state = table.Column<string>(type: "character(2)", fixedLength: true, maxLength: 2, nullable: false),
                    destination_state = table.Column<string>(type: "character(2)", fixedLength: true, maxLength: 2, nullable: false),
                    interstate_rate = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: false),
                    difal_rate = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: false),
                    fcp_rate = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: false),
                    effective_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tax_rules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tax_entries",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    competence_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    tax_rule_id = table.Column<Guid>(type: "uuid", nullable: false),
                    total_order_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    total_order_tax = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    payment_authentication_code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    payment_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tax_entries", x => x.id);
                    table.ForeignKey(
                        name: "fk_taxentry_taxrule_taxruleid",
                        column: x => x.tax_rule_id,
                        principalSchema: "core",
                        principalTable: "tax_rules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_taxentry_competencedate",
                schema: "core",
                table: "tax_entries",
                column: "competence_date");

            migrationBuilder.CreateIndex(
                name: "idx_taxentry_orderid",
                schema: "core",
                table: "tax_entries",
                column: "order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tax_entries_tax_rule_id",
                schema: "core",
                table: "tax_entries",
                column: "tax_rule_id");

            migrationBuilder.CreateIndex(
                name: "idx_taxrules_origin_dest_date",
                schema: "core",
                table: "tax_rules",
                columns: new[] { "origin_state", "destination_state", "effective_date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tax_entries",
                schema: "core");

            migrationBuilder.DropTable(
                name: "tax_rules",
                schema: "core");
        }
    }
}
