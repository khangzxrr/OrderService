using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveShipCostCostPerWeightToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductShipCost_ProductCategory_productCategoryId",
                table: "ProductShipCost");

            migrationBuilder.DropIndex(
                name: "IX_ProductShipCost_productCategoryId",
                table: "ProductShipCost");

            migrationBuilder.DropColumn(
                name: "costPerWeight",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "shipCost",
                table: "OrderDetail");

            migrationBuilder.AddColumn<float>(
                name: "productCostPerWeight",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "productShipCost",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productCostPerWeight",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "productShipCost",
                table: "Product");

            migrationBuilder.AddColumn<float>(
                name: "costPerWeight",
                table: "OrderDetail",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "shipCost",
                table: "OrderDetail",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_ProductShipCost_productCategoryId",
                table: "ProductShipCost",
                column: "productCategoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductShipCost_ProductCategory_productCategoryId",
                table: "ProductShipCost",
                column: "productCategoryId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
