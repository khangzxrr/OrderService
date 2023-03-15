using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameProductToProductHistoryFromOrderDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_ProductHistory_productId",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "OrderDetail",
                newName: "productHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_productId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_productHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_ProductHistory_productHistoryId",
                table: "OrderDetail",
                column: "productHistoryId",
                principalTable: "ProductHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_ProductHistory_productHistoryId",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "productHistoryId",
                table: "OrderDetail",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_productHistoryId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_ProductHistory_productId",
                table: "OrderDetail",
                column: "productId",
                principalTable: "ProductHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
