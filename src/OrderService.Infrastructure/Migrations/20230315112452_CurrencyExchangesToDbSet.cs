using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CurrencyExchangesToDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_CurrencyExchange_currencyExchangeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistory_CurrencyExchange_currencyExchangeId",
                table: "ProductHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrencyExchange",
                table: "CurrencyExchange");

            migrationBuilder.RenameTable(
                name: "CurrencyExchange",
                newName: "CurrencyExchanges");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrencyExchanges",
                table: "CurrencyExchanges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CurrencyExchanges_currencyExchangeId",
                table: "Product",
                column: "currencyExchangeId",
                principalTable: "CurrencyExchanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistory_CurrencyExchanges_currencyExchangeId",
                table: "ProductHistory",
                column: "currencyExchangeId",
                principalTable: "CurrencyExchanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_CurrencyExchanges_currencyExchangeId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductHistory_CurrencyExchanges_currencyExchangeId",
                table: "ProductHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrencyExchanges",
                table: "CurrencyExchanges");

            migrationBuilder.RenameTable(
                name: "CurrencyExchanges",
                newName: "CurrencyExchange");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrencyExchange",
                table: "CurrencyExchange",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CurrencyExchange_currencyExchangeId",
                table: "Product",
                column: "currencyExchangeId",
                principalTable: "CurrencyExchange",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHistory_CurrencyExchange_currencyExchangeId",
                table: "ProductHistory",
                column: "currencyExchangeId",
                principalTable: "CurrencyExchange",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
