using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductReturnFromOD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReturn_OrderDetail_orderDetailId",
                table: "ProductReturn");

            migrationBuilder.DropIndex(
                name: "IX_ProductReturn_orderDetailId",
                table: "ProductReturn");

            migrationBuilder.RenameColumn(
                name: "orderDetailId",
                table: "ProductReturn",
                newName: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturn_productId",
                table: "ProductReturn",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReturn_Product_productId",
                table: "ProductReturn",
                column: "productId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReturn_Product_productId",
                table: "ProductReturn");

            migrationBuilder.DropIndex(
                name: "IX_ProductReturn_productId",
                table: "ProductReturn");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "ProductReturn",
                newName: "orderDetailId");

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
    }
}
