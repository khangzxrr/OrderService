using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductIssueShipping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "productIssueShippingId",
                table: "ProductIssue",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductIssueShipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shipperId = table.Column<int>(type: "int", nullable: false),
                    shippingStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIssueShipping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductIssueShipping_Shippers_shipperId",
                        column: x => x.shipperId,
                        principalTable: "Shippers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductIssue_productIssueShippingId",
                table: "ProductIssue",
                column: "productIssueShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIssueShipping_shipperId",
                table: "ProductIssueShipping",
                column: "shipperId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIssue_ProductIssueShipping_productIssueShippingId",
                table: "ProductIssue",
                column: "productIssueShippingId",
                principalTable: "ProductIssueShipping",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIssue_ProductIssueShipping_productIssueShippingId",
                table: "ProductIssue");

            migrationBuilder.DropTable(
                name: "ProductIssueShipping");

            migrationBuilder.DropIndex(
                name: "IX_ProductIssue_productIssueShippingId",
                table: "ProductIssue");

            migrationBuilder.DropColumn(
                name: "productIssueShippingId",
                table: "ProductIssue");
        }
    }
}
