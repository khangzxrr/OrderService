using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToProductIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyExchanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fromCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyExchanges", x => x.Id);
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
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
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
                    productSellerAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    productSellerEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    productWarrantable = table.Column<bool>(type: "bit", nullable: false),
                    productWarrantyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productWarrantyDuration = table.Column<int>(type: "int", nullable: false),
                    productReturnable = table.Column<bool>(type: "bit", nullable: false),
                    productReturnDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productReturnDuration = table.Column<int>(type: "int", nullable: false),
                    productCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    productResellStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_productCategoryId",
                        column: x => x.productCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductShipCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shipCost = table.Column<float>(type: "real", nullable: false),
                    costPerWeight = table.Column<float>(type: "real", nullable: false),
                    additionalCostCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    passwordSalt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    verify = table.Column<int>(type: "int", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCurrencyExchange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    currencyId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    rate = table.Column<float>(type: "real", nullable: false),
                    productCurrencyCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCurrencyExchange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCurrencyExchange_CurrencyExchanges_currencyId",
                        column: x => x.currencyId,
                        principalTable: "CurrencyExchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCurrencyExchange_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_Users_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductIssue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    assignedEmployeeId = table.Column<int>(type: "int", nullable: false),
                    customerId = table.Column<int>(type: "int", nullable: false),
                    returnReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    returnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isWarranty = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    finishStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    series = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductIssue_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductIssue_Users_assignedEmployeeId",
                        column: x => x.assignedEmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductIssue_Users_customerId",
                        column: x => x.customerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
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
                    table.PrimaryKey("PK_Shippers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shippers_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
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
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    userId = table.Column<int>(type: "int", nullable: false),
                    chatId = table.Column<int>(type: "int", nullable: false),
                    orderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    orderDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    customerDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    deliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactPhonenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingEstimatedDays = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    remainCost = table.Column<double>(type: "float", nullable: false),
                    localShippingStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
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
                        name: "FK_Order_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductIssueId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueMedia_ProductIssue_ProductIssueId",
                        column: x => x.ProductIssueId,
                        principalTable: "ProductIssue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IssuePayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cost = table.Column<float>(type: "real", nullable: false),
                    paymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    paymentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentStatus = table.Column<int>(type: "int", nullable: false),
                    isPaid = table.Column<bool>(type: "bit", nullable: false),
                    ProductIssueId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuePayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssuePayment_ProductIssue_ProductIssueId",
                        column: x => x.ProductIssueId,
                        principalTable: "ProductIssue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    additionalCost = table.Column<float>(type: "real", nullable: false),
                    shipCost = table.Column<float>(type: "real", nullable: false),
                    costPerWeight = table.Column<float>(type: "real", nullable: false),
                    processCost = table.Column<float>(type: "real", nullable: false),
                    totalCost = table.Column<float>(type: "real", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_productId",
                        column: x => x.productId,
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
                    paymentCost = table.Column<double>(type: "float", nullable: false),
                    paymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    paymentDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    transactionalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    shipperId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_OrderShipping_Shippers_shipperId",
                        column: x => x.shipperId,
                        principalTable: "Shippers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chat_employeeId",
                table: "Chat",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ChatId",
                table: "ChatMessage",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueMedia_ProductIssueId",
                table: "IssueMedia",
                column: "ProductIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssuePayment_ProductIssueId",
                table: "IssuePayment",
                column: "ProductIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_chatId",
                table: "Order",
                column: "chatId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_userId",
                table: "Order",
                column: "userId");

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
                name: "IX_Product_productCategoryId",
                table: "Product",
                column: "productCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCurrencyExchange_currencyId",
                table: "ProductCurrencyExchange",
                column: "currencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCurrencyExchange_productId",
                table: "ProductCurrencyExchange",
                column: "productId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductIssue_assignedEmployeeId",
                table: "ProductIssue",
                column: "assignedEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIssue_customerId",
                table: "ProductIssue",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIssue_productId",
                table: "ProductIssue",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShipCost_productCategoryId",
                table: "ProductShipCost",
                column: "productCategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_userId",
                table: "Shippers",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_roleId",
                table: "Users",
                column: "roleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.DropTable(
                name: "IssueMedia");

            migrationBuilder.DropTable(
                name: "IssuePayment");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "OrderPayment");

            migrationBuilder.DropTable(
                name: "OrderShipping");

            migrationBuilder.DropTable(
                name: "ProductCurrencyExchange");

            migrationBuilder.DropTable(
                name: "ProductShipCost");

            migrationBuilder.DropTable(
                name: "ProductIssue");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Shippers");

            migrationBuilder.DropTable(
                name: "CurrencyExchanges");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
