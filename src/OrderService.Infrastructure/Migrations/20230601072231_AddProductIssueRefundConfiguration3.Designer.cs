﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderService.Infrastructure.Data;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230601072231_AddProductIssueRefundConfiguration3")]
    partial class AddProductIssueRefundConfiguration3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

            modelBuilder.Entity("OrderService.Core.CurrencyAggregate.ProductCurrencyExchange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("currencyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("productCurrencyCreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<float>("rate")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("currencyId");

                    b.HasIndex("productId")
                        .IsUnique();

                    b.ToTable("ProductCurrencyExchange");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.Chat", b =>
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

            modelBuilder.Entity("OrderService.Core.OrderAggregate.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ChatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isFromEmployee")
                        .HasColumnType("bit");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("ChatMessage");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.Property<int>("localShippingStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("orderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("orderDescription")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<double>("remainCost")
                        .HasColumnType("float");

                    b.Property<int>("shippingEstimatedDays")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("chatId");

                    b.HasIndex("userId");

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

                    b.Property<float>("costPerWeight")
                        .HasColumnType("real");

                    b.Property<float>("processCost")
                        .HasColumnType("real");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<float>("shipCost")
                        .HasColumnType("real");

                    b.Property<float>("totalCost")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("productId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("OrderService.Core.OrderPaymentAggregate.OrderPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("paymentCost")
                        .HasColumnType("float");

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

                    b.Property<int?>("shipperId")
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

                    b.Property<int>("productCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("productCreateAt")
                        .HasColumnType("datetime2");

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

                    b.Property<int>("productResellStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

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

            modelBuilder.Entity("OrderService.Core.ProductIssueAggregate.ProductIssueRefundConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("productIssueStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<float>("refundRate")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("ProductIssueRefundConfigurations");
                });

            modelBuilder.Entity("OrderService.Core.ProductReturnAggregate.IssueMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ProductIssueId")
                        .HasColumnType("int");

                    b.Property<string>("mediaUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductIssueId");

                    b.ToTable("IssueMedia");
                });

            modelBuilder.Entity("OrderService.Core.ProductReturnAggregate.IssuePayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ProductIssueId")
                        .HasColumnType("int");

                    b.Property<float>("cost")
                        .HasColumnType("real");

                    b.Property<bool>("isPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("paymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("paymentDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("paymentStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductIssueId");

                    b.ToTable("IssuePayment");
                });

            modelBuilder.Entity("OrderService.Core.ProductReturnAggregate.ProductIssue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("assignedEmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("customerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerFullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerPhonenumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("finishStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<bool>("isWarranty")
                        .HasColumnType("bit");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<DateTime>("returnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("returnReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("series")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("assignedEmployeeId");

                    b.HasIndex("productId");

                    b.ToTable("ProductIssue");
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

                    b.ToTable("Shippers");
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

                    b.Property<string>("phoneNumber")
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

            modelBuilder.Entity("OrderService.Core.CurrencyAggregate.ProductCurrencyExchange", b =>
                {
                    b.HasOne("OrderService.Core.CurrencyAggregate.CurrencyExchange", "currency")
                        .WithMany()
                        .HasForeignKey("currencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderService.Core.ProductAggregate.Product", "product")
                        .WithOne("currencyExchange")
                        .HasForeignKey("OrderService.Core.CurrencyAggregate.ProductCurrencyExchange", "productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("currency");

                    b.Navigation("product");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.Chat", b =>
                {
                    b.HasOne("OrderService.Core.UserAggregate.User", "employee")
                        .WithMany()
                        .HasForeignKey("employeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("employee");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.ChatMessage", b =>
                {
                    b.HasOne("OrderService.Core.OrderAggregate.Chat", null)
                        .WithMany("chatMessages")
                        .HasForeignKey("ChatId");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.Order", b =>
                {
                    b.HasOne("OrderService.Core.OrderAggregate.Chat", "chat")
                        .WithMany()
                        .HasForeignKey("chatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderService.Core.UserAggregate.User", "user")
                        .WithMany("orders")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("chat");

                    b.Navigation("user");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.OrderDetail", b =>
                {
                    b.HasOne("OrderService.Core.OrderAggregate.Order", null)
                        .WithMany("orderDetails")
                        .HasForeignKey("OrderId");

                    b.HasOne("OrderService.Core.ProductAggregate.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("OrderService.Core.OrderPaymentAggregate.OrderPayment", b =>
                {
                    b.HasOne("OrderService.Core.OrderAggregate.Order", null)
                        .WithMany("orderPayments")
                        .HasForeignKey("OrderId");
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
                        .HasForeignKey("shipperId");

                    b.Navigation("order");

                    b.Navigation("shipper");
                });

            modelBuilder.Entity("OrderService.Core.ProductAggregate.Product", b =>
                {
                    b.HasOne("OrderService.Core.ProductAggregate.ProductCategory", "productCategory")
                        .WithMany()
                        .HasForeignKey("productCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("OrderService.Core.ProductReturnAggregate.IssueMedia", b =>
                {
                    b.HasOne("OrderService.Core.ProductReturnAggregate.ProductIssue", null)
                        .WithMany("issueMedias")
                        .HasForeignKey("ProductIssueId");
                });

            modelBuilder.Entity("OrderService.Core.ProductReturnAggregate.IssuePayment", b =>
                {
                    b.HasOne("OrderService.Core.ProductReturnAggregate.ProductIssue", null)
                        .WithMany("issuePayments")
                        .HasForeignKey("ProductIssueId");
                });

            modelBuilder.Entity("OrderService.Core.ProductReturnAggregate.ProductIssue", b =>
                {
                    b.HasOne("OrderService.Core.UserAggregate.User", "assignedEmployee")
                        .WithMany()
                        .HasForeignKey("assignedEmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OrderService.Core.ProductAggregate.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("assignedEmployee");

                    b.Navigation("product");
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

            modelBuilder.Entity("OrderService.Core.OrderAggregate.Chat", b =>
                {
                    b.Navigation("chatMessages");
                });

            modelBuilder.Entity("OrderService.Core.OrderAggregate.Order", b =>
                {
                    b.Navigation("orderDetails");

                    b.Navigation("orderPayments");
                });

            modelBuilder.Entity("OrderService.Core.ProductAggregate.Product", b =>
                {
                    b.Navigation("currencyExchange")
                        .IsRequired();
                });

            modelBuilder.Entity("OrderService.Core.ProductAggregate.ProductCategory", b =>
                {
                    b.Navigation("productShipCost")
                        .IsRequired();
                });

            modelBuilder.Entity("OrderService.Core.ProductReturnAggregate.ProductIssue", b =>
                {
                    b.Navigation("issueMedias");

                    b.Navigation("issuePayments");
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
