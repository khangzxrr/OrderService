using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class shippingBoolNowContainStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "queueInShipping",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "localShippingStatus",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "localShippingStatus",
                table: "Order");

            migrationBuilder.AddColumn<bool>(
                name: "queueInShipping",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
