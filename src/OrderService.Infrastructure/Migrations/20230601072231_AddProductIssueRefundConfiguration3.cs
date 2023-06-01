using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductIssueRefundConfiguration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductIssueRefundConfiguration",
                table: "ProductIssueRefundConfiguration");

            migrationBuilder.RenameTable(
                name: "ProductIssueRefundConfiguration",
                newName: "ProductIssueRefundConfigurations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductIssueRefundConfigurations",
                table: "ProductIssueRefundConfigurations",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductIssueRefundConfigurations",
                table: "ProductIssueRefundConfigurations");

            migrationBuilder.RenameTable(
                name: "ProductIssueRefundConfigurations",
                newName: "ProductIssueRefundConfiguration");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductIssueRefundConfiguration",
                table: "ProductIssueRefundConfiguration",
                column: "Id");
        }
    }
}
