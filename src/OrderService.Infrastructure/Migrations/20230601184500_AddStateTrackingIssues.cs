using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStateTrackingIssues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssueStateTracking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    changeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductIssueId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueStateTracking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueStateTracking_ProductIssue_ProductIssueId",
                        column: x => x.ProductIssueId,
                        principalTable: "ProductIssue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueStateTracking_ProductIssueId",
                table: "IssueStateTracking",
                column: "ProductIssueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueStateTracking");
        }
    }
}
