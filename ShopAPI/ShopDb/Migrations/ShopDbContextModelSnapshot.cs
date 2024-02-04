﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShopDb;

#nullable disable

namespace ShopDb.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    partial class ShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ShopDb.Entities.AvailabilityOfProduct", b =>
                {
                    b.Property<string>("Sku")
                        .HasColumnType("text");

                    b.Property<float>("Cost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<int>("Count")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("PackageSizeId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<string>("Size")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("without size");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Sku");

                    b.HasAlternateKey("ProductId", "Size");

                    b.HasIndex("PackageSizeId");

                    b.ToTable("AvailabilityOfProducts");
                });

            modelBuilder.Entity("ShopDb.Entities.Comment", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Stars")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProductId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ShopDb.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float>("AdditionalFees")
                        .HasColumnType("real");

                    b.Property<string>("AdditionalOrderInfo")
                        .HasColumnType("text");

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeliveryInfo")
                        .HasColumnType("text");

                    b.Property<bool>("IsPaidFor")
                        .HasColumnType("boolean");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("WebHookKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ShopDb.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<string>("Sku")
                        .HasColumnType("text");

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.HasKey("OrderId", "Sku");

                    b.HasIndex("ProductId");

                    b.HasIndex("Sku");

                    b.ToTable("OrdersItems");
                });

            modelBuilder.Entity("ShopDb.Entities.PackageSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("Length")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("Width", "Length", "Height");

                    b.ToTable("PackageSizes");
                });

            modelBuilder.Entity("ShopDb.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("AdditionalDetails")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("ShopDb.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("CanBeFound")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PlaceInSearch")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShopDb.Entities.ProductTag", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.HasKey("ProductId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ProductTags");
                });

            modelBuilder.Entity("ShopDb.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("RefreshTokenHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("User");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("ShopDb.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ShopDb.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Noname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("User");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShopDb.Entities.UserShoppingCartItem", b =>
                {
                    b.Property<string>("Sku")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.HasKey("Sku", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersShoppingCartItems");
                });

            modelBuilder.Entity("ShopDb.Entities.AvailabilityOfProduct", b =>
                {
                    b.HasOne("ShopDb.Entities.PackageSize", "PackageSize")
                        .WithMany()
                        .HasForeignKey("PackageSizeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ShopDb.Entities.Product", "Product")
                        .WithMany("AvailabilitisOfProduct")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PackageSize");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShopDb.Entities.Comment", b =>
                {
                    b.HasOne("ShopDb.Entities.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopDb.Entities.User", "User")
                        .WithMany("WrittenComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShopDb.Entities.Order", b =>
                {
                    b.HasOne("ShopDb.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShopDb.Entities.OrderItem", b =>
                {
                    b.HasOne("ShopDb.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("ShopDb.Entities.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("ShopDb.Entities.AvailabilityOfProduct", "AvailabilityOfProduct")
                        .WithMany()
                        .HasForeignKey("Sku")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AvailabilityOfProduct");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShopDb.Entities.Payment", b =>
                {
                    b.HasOne("ShopDb.Entities.Order", "Order")
                        .WithOne("Payment")
                        .HasForeignKey("ShopDb.Entities.Payment", "Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ShopDb.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShopDb.Entities.ProductTag", b =>
                {
                    b.HasOne("ShopDb.Entities.Product", "Product")
                        .WithMany("ProductTags")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopDb.Entities.Tag", "Tag")
                        .WithMany("ProductsTag")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("ShopDb.Entities.Session", b =>
                {
                    b.HasOne("ShopDb.Entities.User", null)
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShopDb.Entities.UserShoppingCartItem", b =>
                {
                    b.HasOne("ShopDb.Entities.AvailabilityOfProduct", "AvailabilityOfProduct")
                        .WithMany("UserShoppingCartItems")
                        .HasForeignKey("Sku")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopDb.Entities.User", null)
                        .WithMany("UserShoppingCartItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AvailabilityOfProduct");
                });

            modelBuilder.Entity("ShopDb.Entities.AvailabilityOfProduct", b =>
                {
                    b.Navigation("UserShoppingCartItems");
                });

            modelBuilder.Entity("ShopDb.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("ShopDb.Entities.Product", b =>
                {
                    b.Navigation("AvailabilitisOfProduct");

                    b.Navigation("Comments");

                    b.Navigation("OrderItems");

                    b.Navigation("ProductTags");
                });

            modelBuilder.Entity("ShopDb.Entities.Tag", b =>
                {
                    b.Navigation("ProductsTag");
                });

            modelBuilder.Entity("ShopDb.Entities.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Sessions");

                    b.Navigation("UserShoppingCartItems");

                    b.Navigation("WrittenComments");
                });
#pragma warning restore 612, 618
        }
    }
}
