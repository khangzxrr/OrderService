using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddtaxsToHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductTax_Product_ProductsId",
                table: "ProductProductTax");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductTax",
                table: "ProductProductTax");

            migrationBuilder.DropIndex(
                name: "IX_ProductProductTax_productTaxesId",
                table: "ProductProductTax");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "ProductProductTax",
                newName: "productsId");

            migrationBuilder.AlterColumn<string>(
                name: "taxName",
                table: "ProductTax",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProductTax",
                table: "ProductProductTax",
                columns: new[] { "productTaxesId", "productsId" });

            migrationBuilder.CreateTable(
                name: "ProductHistoryProductTax",
                columns: table => new
                {
                    productHistoriesId = table.Column<int>(type: "int", nullable: false),
                    productTaxesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistoryProductTax", x => new { x.productHistoriesId, x.productTaxesId });
                    table.ForeignKey(
                        name: "FK_ProductHistoryProductTax_ProductHistory_productHistoriesId",
                        column: x => x.productHistoriesId,
                        principalTable: "ProductHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductHistoryProductTax_ProductTax_productTaxesId",
                        column: x => x.productTaxesId,
                        principalTable: "ProductTax",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductTax_productsId",
                table: "ProductProductTax",
                column: "productsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistoryProductTax_productTaxesId",
                table: "ProductHistoryProductTax",
                column: "productTaxesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductTax_Product_productsId",
                table: "ProductProductTax",
                column: "productsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductTax_Product_productsId",
                table: "ProductProductTax");

            migrationBuilder.DropTable(
                name: "ProductHistoryProductTax");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductTax",
                table: "ProductProductTax");

            migrationBuilder.DropIndex(
                name: "IX_ProductProductTax_productsId",
                table: "ProductProductTax");

            migrationBuilder.RenameColumn(
                name: "productsId",
                table: "ProductProductTax",
                newName: "ProductsId");

            migrationBuilder.AlterColumn<string>(
                name: "taxName",
                table: "ProductTax",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProductTax",
                table: "ProductProductTax",
                columns: new[] { "ProductsId", "productTaxesId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductTax_productTaxesId",
                table: "ProductProductTax",
                column: "productTaxesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductTax_Product_ProductsId",
                table: "ProductProductTax",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
