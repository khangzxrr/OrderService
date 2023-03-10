using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndShipCostToHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_productCategoryId",
                table: "ProductHistory",
                column: "productCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_productShipCostId",
                table: "ProductHistory",
                column: "productShipCostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistory_ProductCategory_productCategoryId",
                table: "ProductHistory",
                column: "productCategoryId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistory_ProductShipCost_productShipCostId",
                table: "ProductHistory",
                column: "productShipCostId",
                principalTable: "ProductShipCost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistory_ProductCategory_productCategoryId",
                table: "ProductHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistory_ProductShipCost_productShipCostId",
                table: "ProductHistory");

            migrationBuilder.DropIndex(
                name: "IX_ProductHistory_productCategoryId",
                table: "ProductHistory");

            migrationBuilder.DropIndex(
                name: "IX_ProductHistory_productShipCostId",
                table: "ProductHistory");
        }
    }
}
