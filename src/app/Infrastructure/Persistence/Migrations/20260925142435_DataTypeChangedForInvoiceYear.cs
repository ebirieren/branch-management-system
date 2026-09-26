using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DataTypeChangedForInvoiceYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.Sql("""
            ALTER TABLE "Invoices"
            ALTER COLUMN "year" TYPE integer
            USING EXTRACT(YEAR FROM "year" AT TIME ZONE 'UTC')::integer;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.Sql("""
            ALTER TABLE "Invoices"
            ALTER COLUMN "year" TYPE timestamp with time zone
            USING make_timestamptz("year", 1, 1, 0, 0, 0, 'UTC');
            """);
        }
    }
}
