using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReConfigProductReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "customerMessage",
                table: "ProductReturn");

            migrationBuilder.DropColumn(
                name: "isExchangeForNewProduct",
                table: "ProductReturn");

            migrationBuilder.DropColumn(
                name: "returnCost",
                table: "ProductReturn");

            migrationBuilder.DropColumn(
                name: "returnQuantity",
                table: "ProductReturn");

            migrationBuilder.DropColumn(
                name: "shippingEstimatedDay",
                table: "ProductReturn");

            migrationBuilder.DropColumn(
                name: "supplierMessage",
                table: "ProductReturn");

            migrationBuilder.RenameColumn(
                name: "shippingStatus",
                table: "ProductReturn",
                newName: "status");

            migrationBuilder.CreateTable(
                name: "ReturnMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductReturnId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnMedia_ProductReturn_ProductReturnId",
                        column: x => x.ProductReturnId,
                        principalTable: "ProductReturn",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReturnSpecificSeriNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seriNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductReturnId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnSpecificSeriNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnSpecificSeriNumber_ProductReturn_ProductReturnId",
                        column: x => x.ProductReturnId,
                        principalTable: "ProductReturn",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturnMedia_ProductReturnId",
                table: "ReturnMedia",
                column: "ProductReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnSpecificSeriNumber_ProductReturnId",
                table: "ReturnSpecificSeriNumber",
                column: "ProductReturnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnMedia");

            migrationBuilder.DropTable(
                name: "ReturnSpecificSeriNumber");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "ProductReturn",
                newName: "shippingStatus");

            migrationBuilder.AddColumn<string>(
                name: "customerMessage",
                table: "ProductReturn",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isExchangeForNewProduct",
                table: "ProductReturn",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "returnCost",
                table: "ProductReturn",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "returnQuantity",
                table: "ProductReturn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "shippingEstimatedDay",
                table: "ProductReturn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "supplierMessage",
                table: "ProductReturn",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
