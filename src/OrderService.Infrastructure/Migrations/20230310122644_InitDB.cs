﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cost = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalCost", x => x.Id);
                });

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
                name: "ProductTax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taxName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    taxPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTax", x => x.Id);
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
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerId = table.Column<int>(type: "int", nullable: true),
                    employeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_Users_customerId",
                        column: x => x.customerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chat_Users_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shipper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shippingDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    shippingEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    shippingStatus = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipper_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productCategoryId = table.Column<int>(type: "int", nullable: false),
                    productShipCostId = table.Column<int>(type: "int", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    productImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productPrice = table.Column<float>(type: "real", nullable: false),
                    productURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productWeight = table.Column<float>(type: "real", nullable: false),
                    productSellerAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    productSellerEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    productWarrantable = table.Column<bool>(type: "bit", nullable: false),
                    productWarrantyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productWarrantyDuration = table.Column<int>(type: "int", nullable: false),
                    productReturnable = table.Column<bool>(type: "bit", nullable: false),
                    productReturnDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productReturnDuration = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chatId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Order_Chat_chatId",
                        column: x => x.chatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productCategoryId = table.Column<int>(type: "int", nullable: false),
                    changeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productPrice = table.Column<float>(type: "real", nullable: false),
                    productURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productWeight = table.Column<float>(type: "real", nullable: false),
                    productShipCostId = table.Column<int>(type: "int", nullable: false),
                    productSellerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productSellerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productWarrantable = table.Column<bool>(type: "bit", nullable: false),
                    productWarrantyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productWarrantyDuration = table.Column<int>(type: "int", nullable: false),
                    productReturnable = table.Column<bool>(type: "bit", nullable: false),
                    productReturnDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productReturnDuration = table.Column<int>(type: "int", nullable: false),
                    currencyExchangeId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductHistory_CurrencyExchange_currencyExchangeId",
                        column: x => x.currencyExchangeId,
                        principalTable: "CurrencyExchange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductHistory_ProductCategory_productCategoryId",
                        column: x => x.productCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductHistory_ProductShipCost_productShipCostId",
                        column: x => x.productShipCostId,
                        principalTable: "ProductShipCost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductHistory_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductProductTax",
                columns: table => new
                {
                    productTaxesId = table.Column<int>(type: "int", nullable: false),
                    productsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductTax", x => new { x.productTaxesId, x.productsId });
                    table.ForeignKey(
                        name: "FK_ProductProductTax_ProductTax_productTaxesId",
                        column: x => x.productTaxesId,
                        principalTable: "ProductTax",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductTax_Product_productsId",
                        column: x => x.productsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "OrderShipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderId = table.Column<int>(type: "int", nullable: false),
                    shipperId = table.Column<int>(type: "int", nullable: false),
                    shippingUsing3rd = table.Column<bool>(type: "bit", nullable: false),
                    orderShippingStatus = table.Column<int>(type: "int", nullable: false),
                    shippingDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    signatureImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShipping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderShipping_Order_orderId",
                        column: x => x.orderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderShipping_Shipper_shipperId",
                        column: x => x.shipperId,
                        principalTable: "Shipper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    additionalCostId = table.Column<int>(type: "int", nullable: false),
                    processCost = table.Column<float>(type: "real", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_AdditionalCost_additionalCostId",
                        column: x => x.additionalCostId,
                        principalTable: "AdditionalCost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetail_ProductHistory_productId",
                        column: x => x.productId,
                        principalTable: "ProductHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductHistoryProductTax",
                columns: table => new
                {
                    productHistoriesId = table.Column<int>(type: "int", nullable: false),
                    productTaxesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistoryProductTax", x => new { x.productHistoriesId, x.productTaxesId });
                    table.ForeignKey(
                        name: "FK_ProductHistoryProductTax_ProductHistory_productHistoriesId",
                        column: x => x.productHistoriesId,
                        principalTable: "ProductHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductHistoryProductTax_ProductTax_productTaxesId",
                        column: x => x.productTaxesId,
                        principalTable: "ProductTax",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReturn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isExchangeForNewProduct = table.Column<bool>(type: "bit", nullable: false),
                    returnQuantity = table.Column<int>(type: "int", nullable: false),
                    returnReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplierMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingStatus = table.Column<int>(type: "int", nullable: false),
                    shippingEstimatedDay = table.Column<int>(type: "int", nullable: false),
                    returnCost = table.Column<float>(type: "real", nullable: false),
                    returnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReturn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReturn_OrderDetail_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReturnPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cost = table.Column<float>(type: "real", nullable: false),
                    paymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    paymentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductReturnId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnPayment_ProductReturn_ProductReturnId",
                        column: x => x.ProductReturnId,
                        principalTable: "ProductReturn",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Order_chatId",
                table: "Order",
                column: "chatId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_additionalCostId",
                table: "OrderDetail",
                column: "additionalCostId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_productId",
                table: "OrderDetail",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayment_OrderId",
                table: "OrderPayment",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderShipping_orderId",
                table: "OrderShipping",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderShipping_shipperId",
                table: "OrderShipping",
                column: "shipperId");

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
                name: "IX_ProductHistory_currencyExchangeId",
                table: "ProductHistory",
                column: "currencyExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_productCategoryId",
                table: "ProductHistory",
                column: "productCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_ProductId",
                table: "ProductHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_productShipCostId",
                table: "ProductHistory",
                column: "productShipCostId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistoryProductTax_productTaxesId",
                table: "ProductHistoryProductTax",
                column: "productTaxesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductTax_productsId",
                table: "ProductProductTax",
                column: "productsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturn_OrderDetailId",
                table: "ProductReturn",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShipCost_productCategoryId",
                table: "ProductShipCost",
                column: "productCategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnPayment_ProductReturnId",
                table: "ReturnPayment",
                column: "ProductReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipper_userId",
                table: "Shipper",
                column: "userId",
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
                name: "ChatMessage");

            migrationBuilder.DropTable(
                name: "Contributors");

            migrationBuilder.DropTable(
                name: "OrderPayment");

            migrationBuilder.DropTable(
                name: "OrderShipping");

            migrationBuilder.DropTable(
                name: "ProductHistoryProductTax");

            migrationBuilder.DropTable(
                name: "ProductProductTax");

            migrationBuilder.DropTable(
                name: "ReturnPayment");

            migrationBuilder.DropTable(
                name: "ToDoItems");

            migrationBuilder.DropTable(
                name: "Shipper");

            migrationBuilder.DropTable(
                name: "ProductTax");

            migrationBuilder.DropTable(
                name: "ProductReturn");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "AdditionalCost");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ProductHistory");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CurrencyExchange");

            migrationBuilder.DropTable(
                name: "ProductShipCost");

            migrationBuilder.DropTable(
                name: "ProductCategory");
        }
    }
}
