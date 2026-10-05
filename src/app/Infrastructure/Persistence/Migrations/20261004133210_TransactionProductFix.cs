using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TransactionProductFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductNameAtTransaction",
                table: "StockTransactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPriceAtTransaction",
                table: "StockTransactions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SupplierNameAtTransaction",
                table: "StockTransactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TaxRateAtTransaction",
                table: "StockTransactions",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductNameAtTransaction",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "ProductPriceAtTransaction",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "SupplierNameAtTransaction",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "TaxRateAtTransaction",
                table: "StockTransactions");
        }
    }
}
