using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_ProductHistory_productHistoryId",
                table: "OrderDetail");

            migrationBuilder.DropTable(
                name: "ProductHistoryProductTax");

            migrationBuilder.DropTable(
                name: "ProductHistory");

            migrationBuilder.RenameColumn(
                name: "productHistoryId",
                table: "OrderDetail",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_productHistoryId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Product_productId",
                table: "OrderDetail",
                column: "productId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Product_productId",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "OrderDetail",
                newName: "productHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_productId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_productHistoryId");

            migrationBuilder.CreateTable(
                name: "ProductHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    currencyExchangeId = table.Column<int>(type: "int", nullable: false),
                    productCategoryId = table.Column<int>(type: "int", nullable: false),
                    productDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productPrice = table.Column<float>(type: "real", nullable: false),
                    productReturnDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productReturnDuration = table.Column<int>(type: "int", nullable: false),
                    productReturnable = table.Column<bool>(type: "bit", nullable: false),
                    productSellerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productSellerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productWarrantable = table.Column<bool>(type: "bit", nullable: false),
                    productWarrantyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productWarrantyDuration = table.Column<int>(type: "int", nullable: false),
                    productWeight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductHistory_CurrencyExchanges_currencyExchangeId",
                        column: x => x.currencyExchangeId,
                        principalTable: "CurrencyExchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductHistory_ProductCategory_productCategoryId",
                        column: x => x.productCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ProductHistory_currencyExchangeId",
                table: "ProductHistory",
                column: "currencyExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_productCategoryId",
                table: "ProductHistory",
                column: "productCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistoryProductTax_productTaxesId",
                table: "ProductHistoryProductTax",
                column: "productTaxesId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_ProductHistory_productHistoryId",
                table: "OrderDetail",
                column: "productHistoryId",
                principalTable: "ProductHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
