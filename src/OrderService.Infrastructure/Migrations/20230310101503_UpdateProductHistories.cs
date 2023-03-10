using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "productReturnDescription",
                table: "ProductHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "productReturnDuration",
                table: "ProductHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "productWarrantyDuration",
                table: "ProductHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "productWarrantyDuration",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "productSellerEmail",
                table: "Product",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "productSellerAddress",
                table: "Product",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<int>(
                name: "productReturnDuration",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productReturnDescription",
                table: "ProductHistory");

            migrationBuilder.DropColumn(
                name: "productReturnDuration",
                table: "ProductHistory");

            migrationBuilder.DropColumn(
                name: "productWarrantyDuration",
                table: "ProductHistory");

            migrationBuilder.AlterColumn<string>(
                name: "productWarrantyDuration",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "productSellerEmail",
                table: "Product",
                type: "real",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<float>(
                name: "productSellerAddress",
                table: "Product",
                type: "real",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "productReturnDuration",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
