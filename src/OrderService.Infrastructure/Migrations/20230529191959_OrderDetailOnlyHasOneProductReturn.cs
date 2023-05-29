using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderDetailOnlyHasOneProductReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReturn_OrderDetail_OrderDetailId",
                table: "ProductReturn");

            migrationBuilder.DropIndex(
                name: "IX_ProductReturn_OrderDetailId",
                table: "ProductReturn");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "OrderDetailId",
                table: "ProductReturn",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturn_OrderDetailId",
                table: "ProductReturn",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReturn_OrderDetail_OrderDetailId",
                table: "ProductReturn",
                column: "OrderDetailId",
                principalTable: "OrderDetail",
                principalColumn: "Id");
        }
    }
}
