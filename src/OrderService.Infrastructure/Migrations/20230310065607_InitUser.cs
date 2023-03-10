using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations;

  /// <inheritdoc />
  public partial class InitUser : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.CreateTable(
              name: "Contributors",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Contributors", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "Projects",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                  Priority = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Projects", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "ToDoItems",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  ContributorId = table.Column<int>(type: "int", nullable: true),
                  IsDone = table.Column<bool>(type: "bit", nullable: false),
                  ProjectId = table.Column<int>(type: "int", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_ToDoItems", x => x.Id);
                  table.ForeignKey(
                      name: "FK_ToDoItems_Projects_ProjectId",
                      column: x => x.ProjectId,
                      principalTable: "Projects",
                      principalColumn: "Id");
              });

          migrationBuilder.CreateIndex(
              name: "IX_ToDoItems_ProjectId",
              table: "ToDoItems",
              column: "ProjectId");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropTable(
              name: "Contributors");

          migrationBuilder.DropTable(
              name: "ToDoItems");

          migrationBuilder.DropTable(
              name: "Projects");
      }
  }
