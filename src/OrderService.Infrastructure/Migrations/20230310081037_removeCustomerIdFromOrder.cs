using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations;

  /// <inheritdoc />
  public partial class removeCustomerIdFromOrder : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropColumn(
              name: "customerId",
              table: "Order");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.AddColumn<int>(
              name: "customerId",
              table: "Order",
              type: "int",
              nullable: true);
      }
  }
