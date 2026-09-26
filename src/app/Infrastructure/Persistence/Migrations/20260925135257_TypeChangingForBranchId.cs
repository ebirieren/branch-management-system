using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TypeChangingForBranchId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Branches_branchId",
                table: "Invoices");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Branches_RowGuid",
                table: "Branches");

            migrationBuilder.Sql("""
                ALTER TABLE "Invoices"
                ADD COLUMN "newBranchId" integer;

                UPDATE "Invoices" AS invoice
                SET "newBranchId" = branch."id"
                FROM "Branches" AS branch
                WHERE invoice."branchId" = branch."RowGuid";

                ALTER TABLE "Invoices"
                DROP COLUMN "branchId";

                ALTER TABLE "Invoices"
                RENAME COLUMN "newBranchId" TO "branchId";

                ALTER TABLE "Invoices"
                ALTER COLUMN "branchId" SET NOT NULL;

                CREATE INDEX "IX_Invoices_branchId"
                ON "Invoices" ("branchId");
                """);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Branches_branchId",
                table: "Invoices",
                column: "branchId",
                principalTable: "Branches",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Branches_branchId",
                table: "Invoices");

            migrationBuilder.Sql("""
                ALTER TABLE "Invoices"
                ADD COLUMN "oldBranchId" uuid;

                UPDATE "Invoices" AS invoice
                SET "oldBranchId" = branch."RowGuid"
                FROM "Branches" AS branch
                WHERE invoice."branchId" = branch."id";

                ALTER TABLE "Invoices"
                DROP COLUMN "branchId";

                ALTER TABLE "Invoices"
                RENAME COLUMN "oldBranchId" TO "branchId";

                ALTER TABLE "Invoices"
                ALTER COLUMN "branchId" SET NOT NULL;

                CREATE INDEX "IX_Invoices_branchId"
                ON "Invoices" ("branchId");
                """);
                
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Branches_RowGuid",
                table: "Branches",
                column: "RowGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Branches_branchId",
                table: "Invoices",
                column: "branchId",
                principalTable: "Branches",
                principalColumn: "RowGuid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
