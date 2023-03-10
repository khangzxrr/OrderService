using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "chatId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerId = table.Column<int>(type: "int", nullable: false),
                    employeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_Users_customerId",
                        column: x => x.customerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chat_Users_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isFromEmployee = table.Column<bool>(type: "bit", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessage_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_chatId",
                table: "Order",
                column: "chatId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_customerId",
                table: "Chat",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_employeeId",
                table: "Chat",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ChatId",
                table: "ChatMessage",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Chat_chatId",
                table: "Order",
                column: "chatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Chat_chatId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Order_chatId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "chatId",
                table: "Order");
        }
    }
}
