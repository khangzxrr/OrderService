using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productCategoryId = table.Column<int>(type: "int", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productPrice = table.Column<float>(type: "real", nullable: false),
                    productURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productWeight = table.Column<float>(type: "real", nullable: false),
                    productShipCostId = table.Column<int>(type: "int", nullable: false),
                    productSellerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productSellerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productWarrantable = table.Column<bool>(type: "bit", nullable: false),
                    productWarrantyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productReturnable = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductHistory_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_ProductId",
                table: "ProductHistory",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductHistory");
        }
    }
}
