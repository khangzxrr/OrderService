﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderService.Infrastructure.Data;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrderService.Core.ChatAggregate.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("employeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("employeeId");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("OrderService.Core.ChatAggregate.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ChatId")
                        .HasColumnType("int");

                    b.Property<bool>("isFromEmployee")
                        .HasColumnType("bit");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("ChatMessage");
                });

            modelBuilder.Entity("OrderService.Core.ContributorAggregate.Contributor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Contributors");
                });

            modelBuilder.Entity("OrderService.Core.CurrencyAggregate.CurrencyExchange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("fromCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("rate")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("CurrencyExchanges");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("chatId")
                        .HasColumnType("int");

                    b.Property<string>("contactPhonenumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerDescription")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("deliveryAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("orderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("orderDescription")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.Property<int>("shippingEstimatedDays")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("chatId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<float>("additionalCost")
                        .HasColumnType("real");

                    b.Property<float>("processCost")
                        .HasColumnType("real");

                    b.Property<float>("productCost")
                        .HasColumnType("real");

                    b.Property<int>("productHistoryId")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<float>("shipCost")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("productHistoryId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.OrderPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<float>("paymentCost")
                        .HasColumnType("real");

                    b.Property<DateTime>("paymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("paymentDescription")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("paymentStatus")
                        .HasColumnType("int");

                    b.Property<string>("transactionalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderPayment");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.ProductHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("currencyExchangeId")
                        .HasColumnType("int");

                    b.Property<int>("productCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("productDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("productPrice")
                        .HasColumnType("real");

                    b.Property<string>("productReturnDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productReturnDuration")
                        .HasColumnType("int");

                    b.Property<bool>("productReturnable")
                        .HasColumnType("bit");

                    b.Property<string>("productSellerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productSellerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("productWarrantable")
                        .HasColumnType("bit");

                    b.Property<string>("productWarrantyDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productWarrantyDuration")
                        .HasColumnType("int");

                    b.Property<float>("productWeight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("currencyExchangeId");

                    b.HasIndex("productCategoryId");

                    b.ToTable("ProductHistory");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.ProductReturn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("OrderDetailId")
                        .HasColumnType("int");

                    b.Property<string>("customerMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isExchangeForNewProduct")
                        .HasColumnType("bit");

                    b.Property<float>("returnCost")
                        .HasColumnType("real");

                    b.Property<DateTime>("returnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("returnQuantity")
                        .HasColumnType("int");

                    b.Property<string>("returnReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("shippingEstimatedDay")
                        .HasColumnType("int");

                    b.Property<int>("shippingStatus")
                        .HasColumnType("int");

                    b.Property<string>("supplierMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderDetailId");

                    b.ToTable("ProductReturn");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.ReturnPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ProductReturnId")
                        .HasColumnType("int");

                    b.Property<float>("cost")
                        .HasColumnType("real");

                    b.Property<DateTime>("paymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("paymentDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductReturnId");

                    b.ToTable("ReturnPayment");
                });

            modelBuilder.Entity("OrderService.Core.OrderShippingAggregate.OrderShipping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("orderId")
                        .HasColumnType("int");

                    b.Property<int>("orderShippingStatus")
                        .HasColumnType("int");

                    b.Property<int>("shipperId")
                        .HasColumnType("int");

                    b.Property<string>("shippingDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("shippingUsing3rd")
                        .HasColumnType("bit");

                    b.Property<string>("signatureImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("orderId");

                    b.HasIndex("shipperId");

                    b.ToTable("OrderShipping");
                });

            modelBuilder.Entity("OrderService.Core.ProductAggregate.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("currencyExchangeId")
                        .HasColumnType("int");

                    b.Property<int>("productCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("productDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<float>("productPrice")
                        .HasColumnType("real");

                    b.Property<string>("productReturnDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productReturnDuration")
                        .HasColumnType("int");

                    b.Property<bool>("productReturnable")
                        .HasColumnType("bit");

                    b.Property<string>("productSellerAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("productSellerEmail")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("productURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("productWarrantable")
                        .HasColumnType("bit");

                    b.Property<string>("productWarrantyDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productWarrantyDuration")
                        .HasColumnType("int");

                    b.Property<float>("productWeight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("currencyExchangeId");

                    b.HasIndex("productCategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("OrderService.Core.ProductAggregate.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("productCategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("OrderService.Core.ProductAggregate.ProductShipCost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("additionalCostCondition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("costPerWeight")
                        .HasColumnType("real");

                    b.Property<int>("productCategoryId")
                        .HasColumnType("int");

                    b.Property<float>("shipCost")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("productCategoryId")
                        .IsUnique();

                    b.ToTable("ProductShipCost");
                });

            modelBuilder.Entity("OrderService.Core.ProductAggregate.ProductTax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("taxName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("taxPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("ProductTax");
                });

            modelBuilder.Entity("OrderService.Core.ProjectAggregate.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("OrderService.Core.ProjectAggregate.ToDoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ContributorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ToDoItems");
                });

            modelBuilder.Entity("OrderService.Core.ShipperAggregate.Shipper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("shippingDistrict")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("shippingEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("shippingStartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("shippingStatus")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("Shipper");
                });

            modelBuilder.Entity("OrderService.Core.UserAggregate.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("roleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("OrderService.Core.UserAggregate.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("dateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("firstname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("guid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("passwordHash")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("passwordSalt")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("roleId")
                        .HasColumnType("int");

                    b.Property<int>("verify")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("roleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProductHistoryProductTax", b =>
                {
                    b.Property<int>("productHistoriesId")
                        .HasColumnType("int");

                    b.Property<int>("productTaxesId")
                        .HasColumnType("int");

                    b.HasKey("productHistoriesId", "productTaxesId");

                    b.HasIndex("productTaxesId");

                    b.ToTable("ProductHistoryProductTax");
                });

            modelBuilder.Entity("ProductProductTax", b =>
                {
                    b.Property<int>("productTaxesId")
                        .HasColumnType("int");

                    b.Property<int>("productsId")
                        .HasColumnType("int");

                    b.HasKey("productTaxesId", "productsId");

                    b.HasIndex("productsId");

                    b.ToTable("ProductProductTax");
                });

            modelBuilder.Entity("OrderService.Core.ChatAggregate.Chat", b =>
                {
                    b.HasOne("OrderService.Core.UserAggregate.User", "employee")
                        .WithMany()
                        .HasForeignKey("employeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("employee");
                });

            modelBuilder.Entity("OrderService.Core.ChatAggregate.ChatMessage", b =>
                {
                    b.HasOne("OrderService.Core.ChatAggregate.Chat", null)
                        .WithMany("chatMessages")
                        .HasForeignKey("ChatId");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.Order", b =>
                {
                    b.HasOne("OrderService.Core.UserAggregate.User", null)
                        .WithMany("orders")
                        .HasForeignKey("UserId");

                    b.HasOne("OrderService.Core.ChatAggregate.Chat", "chat")
                        .WithMany()
                        .HasForeignKey("chatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("chat");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.OrderDetail", b =>
                {
                    b.HasOne("OrderService.Core.OrderAggregate.Order", null)
                        .WithMany("orderDetails")
                        .HasForeignKey("OrderId");

                    b.HasOne("OrderService.Core.OrderAggregate.ProductHistory", "productHistory")
                        .WithMany()
                        .HasForeignKey("productHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("productHistory");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.OrderPayment", b =>
                {
                    b.HasOne("OrderService.Core.OrderAggregate.Order", null)
                        .WithMany("orderPayments")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.ProductHistory", b =>
                {
                    b.HasOne("OrderService.Core.CurrencyAggregate.CurrencyExchange", "currencyExchange")
                        .WithMany()
                        .HasForeignKey("currencyExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderService.Core.ProductAggregate.ProductCategory", "productCategory")
                        .WithMany()
                        .HasForeignKey("productCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("currencyExchange");

                    b.Navigation("productCategory");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.ProductReturn", b =>
                {
                    b.HasOne("OrderService.Core.OrderAggregate.OrderDetail", null)
                        .WithMany("productReturns")
                        .HasForeignKey("OrderDetailId");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.ReturnPayment", b =>
                {
                    b.HasOne("OrderService.Core.OrderAggregate.ProductReturn", null)
                        .WithMany("returnPayments")
                        .HasForeignKey("ProductReturnId");
                });

            modelBuilder.Entity("OrderService.Core.OrderShippingAggregate.OrderShipping", b =>
                {
                    b.HasOne("OrderService.Core.OrderAggregate.Order", "order")
                        .WithMany()
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderService.Core.ShipperAggregate.Shipper", "shipper")
                        .WithMany("OrderShippings")
                        .HasForeignKey("shipperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("order");

                    b.Navigation("shipper");
                });

            modelBuilder.Entity("OrderService.Core.ProductAggregate.Product", b =>
                {
                    b.HasOne("OrderService.Core.CurrencyAggregate.CurrencyExchange", "currencyExchange")
                        .WithMany()
                        .HasForeignKey("currencyExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderService.Core.ProductAggregate.ProductCategory", "productCategory")
                        .WithMany()
                        .HasForeignKey("productCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("currencyExchange");

                    b.Navigation("productCategory");
                });

            modelBuilder.Entity("OrderService.Core.ProductAggregate.ProductShipCost", b =>
                {
                    b.HasOne("OrderService.Core.ProductAggregate.ProductCategory", null)
                        .WithOne("productShipCost")
                        .HasForeignKey("OrderService.Core.ProductAggregate.ProductShipCost", "productCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrderService.Core.ProjectAggregate.ToDoItem", b =>
                {
                    b.HasOne("OrderService.Core.ProjectAggregate.Project", null)
                        .WithMany("Items")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("OrderService.Core.ShipperAggregate.Shipper", b =>
                {
                    b.HasOne("OrderService.Core.UserAggregate.User", "user")
                        .WithOne("shipper")
                        .HasForeignKey("OrderService.Core.ShipperAggregate.Shipper", "userId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("OrderService.Core.UserAggregate.User", b =>
                {
                    b.HasOne("OrderService.Core.UserAggregate.Role", "role")
                        .WithMany()
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("role");
                });

            modelBuilder.Entity("ProductHistoryProductTax", b =>
                {
                    b.HasOne("OrderService.Core.OrderAggregate.ProductHistory", null)
                        .WithMany()
                        .HasForeignKey("productHistoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderService.Core.ProductAggregate.ProductTax", null)
                        .WithMany()
                        .HasForeignKey("productTaxesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductProductTax", b =>
                {
                    b.HasOne("OrderService.Core.ProductAggregate.ProductTax", null)
                        .WithMany()
                        .HasForeignKey("productTaxesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderService.Core.ProductAggregate.Product", null)
                        .WithMany()
                        .HasForeignKey("productsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrderService.Core.ChatAggregate.Chat", b =>
                {
                    b.Navigation("chatMessages");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.Order", b =>
                {
                    b.Navigation("orderDetails");

                    b.Navigation("orderPayments");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.OrderDetail", b =>
                {
                    b.Navigation("productReturns");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.ProductReturn", b =>
                {
                    b.Navigation("returnPayments");
                });

            modelBuilder.Entity("OrderService.Core.ProductAggregate.ProductCategory", b =>
                {
                    b.Navigation("productShipCost")
                        .IsRequired();
                });

            modelBuilder.Entity("OrderService.Core.ProjectAggregate.Project", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("OrderService.Core.ShipperAggregate.Shipper", b =>
                {
                    b.Navigation("OrderShippings");
                });

            modelBuilder.Entity("OrderService.Core.UserAggregate.User", b =>
                {
                    b.Navigation("orders");

                    b.Navigation("shipper");
                });
#pragma warning restore 612, 618
        }
    }
}
