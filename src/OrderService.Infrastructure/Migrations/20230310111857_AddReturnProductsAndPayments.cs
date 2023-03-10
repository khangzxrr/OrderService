using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReturnProductsAndPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductReturn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isExchangeForNewProduct = table.Column<bool>(type: "bit", nullable: false),
                    returnQuantity = table.Column<int>(type: "int", nullable: false),
                    returnReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplierMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingStatus = table.Column<int>(type: "int", nullable: false),
                    shippingEstimatedDay = table.Column<int>(type: "int", nullable: false),
                    returnCost = table.Column<float>(type: "real", nullable: false),
                    returnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReturn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReturn_OrderDetail_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReturnPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cost = table.Column<float>(type: "real", nullable: false),
                    paymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    paymentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductReturnId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnPayment_ProductReturn_ProductReturnId",
                        column: x => x.ProductReturnId,
                        principalTable: "ProductReturn",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturn_OrderDetailId",
                table: "ProductReturn",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnPayment_ProductReturnId",
                table: "ReturnPayment",
                column: "ProductReturnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnPayment");

            migrationBuilder.DropTable(
                name: "ProductReturn");
        }
    }
}
