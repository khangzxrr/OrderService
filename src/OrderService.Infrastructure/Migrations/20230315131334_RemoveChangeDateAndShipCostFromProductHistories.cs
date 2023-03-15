using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveChangeDateAndShipCostFromProductHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistory_ProductShipCost_productShipCostId",
                table: "ProductHistory");

            migrationBuilder.DropIndex(
                name: "IX_ProductHistory_productShipCostId",
                table: "ProductHistory");

            migrationBuilder.DropColumn(
                name: "productShipCostId",
                table: "ProductHistory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "productShipCostId",
                table: "ProductHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_productShipCostId",
                table: "ProductHistory",
                column: "productShipCostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistory_ProductShipCost_productShipCostId",
                table: "ProductHistory",
                column: "productShipCostId",
                principalTable: "ProductShipCost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
