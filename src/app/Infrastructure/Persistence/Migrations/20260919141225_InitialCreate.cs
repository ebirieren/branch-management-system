using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    branchName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    productId = table.Column<Guid>(type: "uuid", nullable: false),
                    invoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.id);
                    table.UniqueConstraint("AK_Branches_RowGuid", x => x.RowGuid);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    productName = table.Column<string>(type: "text", nullable: false),
                    productPrice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    previousProductPrice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    productCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    taxRate = table.Column<int>(type: "integer", nullable: true),
                    productPriceWithTax = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    branchId = table.Column<Guid>(type: "uuid", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    branchId = table.Column<Guid>(type: "uuid", nullable: false),
                    term = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.id);
                    table.ForeignKey(
                        name: "FK_Invoices_Branches_branchId",
                        column: x => x.branchId,
                        principalTable: "Branches",
                        principalColumn: "RowGuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchProduct",
                columns: table => new
                {
                    BranchesId = table.Column<int>(type: "integer", nullable: false),
                    ProductsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchProduct", x => new { x.BranchesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_BranchProduct_Branches_BranchesId",
                        column: x => x.BranchesId,
                        principalTable: "Branches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_branchName",
                table: "Branches",
                column: "branchName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_id",
                table: "Branches",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BranchProduct_ProductsId",
                table: "BranchProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_branchId",
                table: "Invoices",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_id",
                table: "Invoices",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_id",
                table: "Products",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_productName",
                table: "Products",
                column: "productName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchProduct");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
