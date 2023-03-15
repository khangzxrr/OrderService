using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAdditionalCostFromOrderDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_AdditionalCost_additionalCostId",
                table: "OrderDetail");

            migrationBuilder.DropTable(
                name: "AdditionalCost");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_additionalCostId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "additionalCostId",
                table: "OrderDetail");

            migrationBuilder.AddColumn<string>(
                name: "additionalCostCondition",
                table: "ProductShipCost",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "additionalCost",
                table: "OrderDetail",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "additionalCostCondition",
                table: "ProductShipCost");

            migrationBuilder.DropColumn(
                name: "additionalCost",
                table: "OrderDetail");

            migrationBuilder.AddColumn<int>(
                name: "additionalCostId",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdditionalCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cost = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalCost", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_additionalCostId",
                table: "OrderDetail",
                column: "additionalCostId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_AdditionalCost_additionalCostId",
                table: "OrderDetail",
                column: "additionalCostId",
                principalTable: "AdditionalCost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
