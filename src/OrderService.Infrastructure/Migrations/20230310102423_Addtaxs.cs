using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addtaxs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductTax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taxName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductTax",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    productTaxesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductTax", x => new { x.ProductsId, x.productTaxesId });
                    table.ForeignKey(
                        name: "FK_ProductProductTax_ProductTax_productTaxesId",
                        column: x => x.productTaxesId,
                        principalTable: "ProductTax",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductTax_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductTax_productTaxesId",
                table: "ProductProductTax",
                column: "productTaxesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProductTax");

            migrationBuilder.DropTable(
                name: "ProductTax");
        }
    }
}
