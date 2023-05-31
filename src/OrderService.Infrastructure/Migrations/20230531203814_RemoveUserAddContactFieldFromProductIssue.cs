using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserAddContactFieldFromProductIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIssue_Users_customerId",
                table: "ProductIssue");

            migrationBuilder.DropIndex(
                name: "IX_ProductIssue_customerId",
                table: "ProductIssue");

            migrationBuilder.DropColumn(
                name: "customerId",
                table: "ProductIssue");

            migrationBuilder.AddColumn<string>(
                name: "customerEmail",
                table: "ProductIssue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "customerFullname",
                table: "ProductIssue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "customerPhonenumber",
                table: "ProductIssue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "customerEmail",
                table: "ProductIssue");

            migrationBuilder.DropColumn(
                name: "customerFullname",
                table: "ProductIssue");

            migrationBuilder.DropColumn(
                name: "customerPhonenumber",
                table: "ProductIssue");

            migrationBuilder.AddColumn<int>(
                name: "customerId",
                table: "ProductIssue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductIssue_customerId",
                table: "ProductIssue",
                column: "customerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIssue_Users_customerId",
                table: "ProductIssue",
                column: "customerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
