using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct2 : Migration
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
                name: "CurrencyExchange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fromCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyExchange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productCategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.Id);
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    passwordSalt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    verify = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductShipCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cost = table.Column<float>(type: "real", nullable: false),
                    productCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShipCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductShipCost_ProductCategory_productCategoryId",
                        column: x => x.productCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    orderDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    customerDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    deliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactPhonenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingEstimatedDays = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productCategoryId = table.Column<int>(type: "int", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    productImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productPrice = table.Column<float>(type: "real", nullable: false),
                    productURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productWeight = table.Column<float>(type: "real", nullable: false),
                    productShipCostId = table.Column<int>(type: "int", nullable: false),
                    productSellerAddress = table.Column<float>(type: "real", maxLength: 200, nullable: false),
                    productSellerEmail = table.Column<float>(type: "real", maxLength: 200, nullable: false),
                    productWarrantable = table.Column<bool>(type: "bit", nullable: false),
                    productWarrantyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productWarrantyDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productReturnable = table.Column<bool>(type: "bit", nullable: false),
                    productReturnDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productReturnDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    currencyExchangeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_CurrencyExchange_currencyExchangeId",
                        column: x => x.currencyExchangeId,
                        principalTable: "CurrencyExchange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_productCategoryId",
                        column: x => x.productCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_ProductShipCost_productShipCostId",
                        column: x => x.productShipCostId,
                        principalTable: "ProductShipCost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentStatus = table.Column<int>(type: "int", nullable: false),
                    paymentCost = table.Column<float>(type: "real", nullable: false),
                    paymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    paymentDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPayment_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayment_OrderId",
                table: "OrderPayment",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_currencyExchangeId",
                table: "Product",
                column: "currencyExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_productCategoryId",
                table: "Product",
                column: "productCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_productShipCostId",
                table: "Product",
                column: "productShipCostId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShipCost_productCategoryId",
                table: "ProductShipCost",
                column: "productCategoryId",
                unique: true);

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
                name: "OrderPayment");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ToDoItems");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "CurrencyExchange");

            migrationBuilder.DropTable(
                name: "ProductShipCost");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ProductCategory");
        }
    }
}
