using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnSpecificSeriNumber");

            migrationBuilder.AddColumn<string>(
                name: "series",
                table: "ProductReturn",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "series",
                table: "ProductReturn");

            migrationBuilder.CreateTable(
                name: "ReturnSpecificSeriNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductReturnId = table.Column<int>(type: "int", nullable: true),
                    seriNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnSpecificSeriNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnSpecificSeriNumber_ProductReturn_ProductReturnId",
                        column: x => x.ProductReturnId,
                        principalTable: "ProductReturn",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturnSpecificSeriNumber_ProductReturnId",
                table: "ReturnSpecificSeriNumber",
                column: "ProductReturnId");
        }
    }
}
