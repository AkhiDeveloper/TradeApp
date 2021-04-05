﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TradeApp.Data;

namespace TradeApp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210331104502_UserTable2")]
    partial class UserTable2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TradeApp.Data.Cart", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CustomerId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("TradeApp.Data.CartProduct", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CartCustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("productCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("quantity")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.HasIndex("CartCustomerId");

                    b.HasIndex("productCode");

                    b.ToTable("CartProducts");
                });

            modelBuilder.Entity("TradeApp.Data.CustomerProductRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("customerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("productCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("productrating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("customerId");

                    b.HasIndex("productCode");

                    b.ToTable("CustomerProductRatings");
                });

            modelBuilder.Entity("TradeApp.Data.CustomerWishlist", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Code");

                    b.HasIndex("customerId");

                    b.ToTable("CustomerWishlists");
                });

            modelBuilder.Entity("TradeApp.Data.Order", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PlacedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("customerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Code");

                    b.HasIndex("customerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TradeApp.Data.OrderProduct", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OrderCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("productCode")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Code");

                    b.HasIndex("OrderCode");

                    b.HasIndex("productCode");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("TradeApp.Data.Product", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerWishlistCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MRP")
                        .HasColumnType("money");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.HasIndex("CustomerWishlistCode");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("TradeApp.Data.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("TradeApp.Data.Admin", b =>
                {
                    b.HasBaseType("TradeApp.Data.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("TradeApp.Data.Customer", b =>
                {
                    b.HasBaseType("TradeApp.Data.User");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TradeApp.Data.Cart", b =>
                {
                    b.HasOne("TradeApp.Data.Customer", "customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");
                });

            modelBuilder.Entity("TradeApp.Data.CartProduct", b =>
                {
                    b.HasOne("TradeApp.Data.Cart", "Cart")
                        .WithMany("cartProducts")
                        .HasForeignKey("CartCustomerId");

                    b.HasOne("TradeApp.Data.Product", "product")
                        .WithMany()
                        .HasForeignKey("productCode");

                    b.Navigation("Cart");

                    b.Navigation("product");
                });

            modelBuilder.Entity("TradeApp.Data.CustomerProductRating", b =>
                {
                    b.HasOne("TradeApp.Data.Customer", "customer")
                        .WithMany()
                        .HasForeignKey("customerId");

                    b.HasOne("TradeApp.Data.Product", "product")
                        .WithMany()
                        .HasForeignKey("productCode");

                    b.Navigation("customer");

                    b.Navigation("product");
                });

            modelBuilder.Entity("TradeApp.Data.CustomerWishlist", b =>
                {
                    b.HasOne("TradeApp.Data.Customer", "customer")
                        .WithMany()
                        .HasForeignKey("customerId");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("TradeApp.Data.Order", b =>
                {
                    b.HasOne("TradeApp.Data.Customer", "customer")
                        .WithMany()
                        .HasForeignKey("customerId");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("TradeApp.Data.OrderProduct", b =>
                {
                    b.HasOne("TradeApp.Data.Order", "Order")
                        .WithMany("orderproducts")
                        .HasForeignKey("OrderCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TradeApp.Data.Product", "product")
                        .WithMany()
                        .HasForeignKey("productCode");

                    b.Navigation("Order");

                    b.Navigation("product");
                });

            modelBuilder.Entity("TradeApp.Data.Product", b =>
                {
                    b.HasOne("TradeApp.Data.CustomerWishlist", null)
                        .WithMany("products")
                        .HasForeignKey("CustomerWishlistCode");
                });

            modelBuilder.Entity("TradeApp.Data.Cart", b =>
                {
                    b.Navigation("cartProducts");
                });

            modelBuilder.Entity("TradeApp.Data.CustomerWishlist", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("TradeApp.Data.Order", b =>
                {
                    b.Navigation("orderproducts");
                });
#pragma warning restore 612, 618
        }
    }
}