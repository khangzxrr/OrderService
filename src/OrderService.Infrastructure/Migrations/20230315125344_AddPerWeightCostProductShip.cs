using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPerWeightCostProductShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cost",
                table: "ProductShipCost",
                newName: "shipCost");

            migrationBuilder.AddColumn<float>(
                name: "costPerWeight",
                table: "ProductShipCost",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "costPerWeight",
                table: "ProductShipCost");

            migrationBuilder.RenameColumn(
                name: "shipCost",
                table: "ProductShipCost",
                newName: "cost");
        }
    }
}
