using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderDetailOnlyHasOneProductReturnOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_ProductReturn_productReturnId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_productReturnId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "productReturnId",
                table: "OrderDetail");

            migrationBuilder.AddColumn<int>(
                name: "orderDetailId",
                table: "ProductReturn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturn_orderDetailId",
                table: "ProductReturn",
                column: "orderDetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReturn_OrderDetail_orderDetailId",
                table: "ProductReturn",
                column: "orderDetailId",
                principalTable: "OrderDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReturn_OrderDetail_orderDetailId",
                table: "ProductReturn");

            migrationBuilder.DropIndex(
                name: "IX_ProductReturn_orderDetailId",
                table: "ProductReturn");

            migrationBuilder.DropColumn(
                name: "orderDetailId",
                table: "ProductReturn");

            migrationBuilder.AddColumn<int>(
                name: "productReturnId",
                table: "OrderDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_productReturnId",
                table: "OrderDetail",
                column: "productReturnId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_ProductReturn_productReturnId",
                table: "OrderDetail",
                column: "productReturnId",
                principalTable: "ProductReturn",
                principalColumn: "Id");
        }
    }
}
