using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductShipCostFromProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductShipCost_productShipCostId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_productShipCostId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "productShipCostId",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "productShipCostId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_productShipCostId",
                table: "Product",
                column: "productShipCostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductShipCost_productShipCostId",
                table: "Product",
                column: "productShipCostId",
                principalTable: "ProductShipCost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
