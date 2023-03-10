using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyExchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "currencyExchangeId",
                table: "ProductHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_currencyExchangeId",
                table: "ProductHistory",
                column: "currencyExchangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistory_CurrencyExchange_currencyExchangeId",
                table: "ProductHistory",
                column: "currencyExchangeId",
                principalTable: "CurrencyExchange",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistory_CurrencyExchange_currencyExchangeId",
                table: "ProductHistory");

            migrationBuilder.DropIndex(
                name: "IX_ProductHistory_currencyExchangeId",
                table: "ProductHistory");

            migrationBuilder.DropColumn(
                name: "currencyExchangeId",
                table: "ProductHistory");
        }
    }
}
