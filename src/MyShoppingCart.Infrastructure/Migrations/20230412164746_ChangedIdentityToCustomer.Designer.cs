﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyShoppingCart.Infrastructure;

#nullable disable

namespace MyShoppingCart.Infrastructure.Migrations
{
    [DbContext(typeof(MyShoppingCartContext))]
    [Migration("20230412164746_ChangedIdentityToCustomer")]
    partial class ChangedIdentityToCustomer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken", (string)null);
                });

            modelBuilder.Entity("MyShoppingCart.Domain.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.ToTable("Address", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("786de95e-2d4c-4524-ac64-6ddf11ad9ec5"),
                            City = "Bedrock",
                            PostalCode = "12345",
                            State = "MO",
                            Street = "123 Test St"
                        },
                        new
                        {
                            Id = new Guid("6b760260-799c-4af1-a173-0bf83a2a74d5"),
                            City = "Bedrock",
                            PostalCode = "12345",
                            State = "MO",
                            Street = "123 Test St"
                        },
                        new
                        {
                            Id = new Guid("b592fa04-541a-4bf2-967c-c07468af2014"),
                            City = "Space City",
                            PostalCode = "12345",
                            State = "MO",
                            Street = "123 Test St"
                        },
                        new
                        {
                            Id = new Guid("ccb9f54b-f5a0-4d42-927d-c65294e0f629"),
                            City = "Space City",
                            PostalCode = "12345",
                            State = "MO",
                            Street = "123 Test St"
                        });
                });

            modelBuilder.Entity("MyShoppingCart.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("BillingAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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

                    b.Property<Guid?>("ShippingAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.HasIndex("BillingAddressId")
                        .IsUnique()
                        .HasFilter("[BillingAddressId] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ShippingAddressId")
                        .IsUnique()
                        .HasFilter("[ShippingAddressId] IS NOT NULL");

                    b.ToTable("Customer", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"),
                            AccessFailedCount = 0,
                            BillingAddressId = new Guid("786de95e-2d4c-4524-ac64-6ddf11ad9ec5"),
                            ConcurrencyStamp = "e650311e-8a38-4c6b-a0fc-13c99b4f70da",
                            Email = "fred.flintstone@test.com",
                            EmailConfirmed = false,
                            FirstName = "Fred",
                            LastName = "Flintstone",
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            ShippingAddressId = new Guid("6b760260-799c-4af1-a173-0bf83a2a74d5"),
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"),
                            AccessFailedCount = 0,
                            BillingAddressId = new Guid("b592fa04-541a-4bf2-967c-c07468af2014"),
                            ConcurrencyStamp = "2cb4f00c-1f86-4924-a965-a42e55067f1f",
                            Email = "george.jetson@test.com",
                            EmailConfirmed = false,
                            FirstName = "George",
                            LastName = "Jetson",
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            ShippingAddressId = new Guid("ccb9f54b-f5a0-4d42-927d-c65294e0f629"),
                            TwoFactorEnabled = false
                        });
                });

            modelBuilder.Entity("MyShoppingCart.Domain.Entities.LineItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("LineItem", (string)null);
                });

            modelBuilder.Entity("MyShoppingCart.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDateTimeUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.HasIndex("CustomerId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("MyShoppingCart.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("7bc8ae1b-031a-4f3a-815c-2111288ff58c"),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.",
                            Name = "Nike Tennis Shoes",
                            Price = 100.00m
                        },
                        new
                        {
                            Id = new Guid("9955f4d7-3e40-4111-a76d-23406f93334b"),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.",
                            Name = "Fruit Stripe Gum",
                            Price = 1.99m
                        },
                        new
                        {
                            Id = new Guid("ad7d0cf7-ce00-477d-ae2a-5691f65eba0e"),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.",
                            Name = "Cheerios",
                            Price = 6.00m
                        },
                        new
                        {
                            Id = new Guid("e226d6b2-324f-4508-b5e5-0db77b345c69"),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.",
                            Name = "7Up",
                            Price = 1.50m
                        },
                        new
                        {
                            Id = new Guid("e3b2bcce-a8f4-4f7e-9c9e-6ac93e03554a"),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.",
                            Name = "A Plaid Flannel Shirt",
                            Price = 20.00m
                        },
                        new
                        {
                            Id = new Guid("1caa7fb0-8c2e-4304-a1ec-747a89623131"),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.",
                            Name = "Garbage Pale Kids Stickers",
                            Price = 4.00m
                        },
                        new
                        {
                            Id = new Guid("a9c15177-e1a4-4dc8-bcb0-5d78128fdeae"),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.",
                            Name = "Pink Stuffed Dinosaur",
                            Price = 15.99m
                        },
                        new
                        {
                            Id = new Guid("24ef70c3-0fc1-48d7-994f-380d4c533419"),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.",
                            Name = "Black Trenchcoat",
                            Price = 100.00m
                        },
                        new
                        {
                            Id = new Guid("2df1a80e-651a-417a-9028-b81d30a9a26e"),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.",
                            Name = "Monopoly",
                            Price = 100.00m
                        },
                        new
                        {
                            Id = new Guid("0553ca62-284d-4379-afc5-c2d4903f7a4c"),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.",
                            ImageUrl = "/img/dog_collar.jpg",
                            Name = "A Dog Collar",
                            Price = 100.00m
                        });
                });

            modelBuilder.Entity("MyShoppingCart.Domain.Entities.SecurityClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.ToTable("Claim", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("7da154b4-0861-416c-a1c2-445eb99f4b27"),
                            CustomerId = new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"),
                            Type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                            Value = "4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"
                        },
                        new
                        {
                            Id = new Guid("cb359c6c-4bb7-41e5-96b6-947bfb03f232"),
                            CustomerId = new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"),
                            Type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                            Value = "Customer"
                        },
                        new
                        {
                            Id = new Guid("1ec4abf1-2427-4971-ac32-69fc9e3034d7"),
                            CustomerId = new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"),
                            Type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                            Value = "79F42C77-83E5-403B-9EC1-6A3FF285C5AC"
                        },
                        new
                        {
                            Id = new Guid("f56e3465-0de3-41c7-a855-9e3be6862896"),
                            CustomerId = new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"),
                            Type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                            Value = "Admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("MyShoppingCart.Domain.Entities.Customer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("MyShoppingCart.Domain.Entities.Customer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyShoppingCart.Domain.Entities.Customer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("MyShoppingCart.Domain.Entities.Customer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyShoppingCart.Domain.Entities.Customer", b =>
                {
                    b.HasOne("MyShoppingCart.Domain.Entities.Address", "BillingAddress")
                        .WithOne()
                        .HasForeignKey("MyShoppingCart.Domain.Entities.Customer", "BillingAddressId");

                    b.HasOne("MyShoppingCart.Domain.Entities.Address", "ShippingAddress")
                        .WithOne()
                        .HasForeignKey("MyShoppingCart.Domain.Entities.Customer", "ShippingAddressId");

                    b.Navigation("BillingAddress");

                    b.Navigation("ShippingAddress");
                });

            modelBuilder.Entity("MyShoppingCart.Domain.Entities.LineItem", b =>
                {
                    b.HasOne("MyShoppingCart.Domain.Entities.Order", null)
                        .WithMany("LineItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyShoppingCart.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MyShoppingCart.Domain.Entities.Order", b =>
                {
                    b.HasOne("MyShoppingCart.Domain.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("MyShoppingCart.Domain.Entities.Order", b =>
                {
                    b.Navigation("LineItems");
                });
#pragma warning restore 612, 618
        }
    }
}
