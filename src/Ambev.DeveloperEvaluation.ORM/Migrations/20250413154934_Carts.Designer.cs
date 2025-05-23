﻿// <auto-generated />
using System;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20250413154934_Carts")]
    partial class Carts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CanceledAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Carts", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CanceledAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CartId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("CartId", "ProductId")
                        .IsUnique();

                    b.ToTable("CartItems", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Cart", b =>
                {
                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.CartItem", b =>
                {
                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Cart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.MonetaryValue", "TotalAmount", b1 =>
                        {
                            b1.Property<int>("CartItemId")
                                .HasColumnType("integer");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("TotalAmount");

                            b1.HasKey("CartItemId");

                            b1.ToTable("CartItems");

                            b1.WithOwner()
                                .HasForeignKey("CartItemId");
                        });

                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.MonetaryValue", "UnitPrice", b1 =>
                        {
                            b1.Property<int>("CartItemId")
                                .HasColumnType("integer");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("UnitPrice");

                            b1.HasKey("CartItemId");

                            b1.ToTable("CartItems");

                            b1.WithOwner()
                                .HasForeignKey("CartItemId");
                        });

                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.PercentageValue", "DiscountPercent", b1 =>
                        {
                            b1.Property<int>("CartItemId")
                                .HasColumnType("integer");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("DiscountAmount");

                            b1.HasKey("CartItemId");

                            b1.ToTable("CartItems");

                            b1.WithOwner()
                                .HasForeignKey("CartItemId");
                        });

                    b.Navigation("Cart");

                    b.Navigation("DiscountPercent")
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("TotalAmount")
                        .IsRequired();

                    b.Navigation("UnitPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Product", b =>
                {
                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.MonetaryValue", "Price", b1 =>
                        {
                            b1.Property<int>("ProductId")
                                .HasColumnType("integer");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("Price");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.RatingValue", "Rating", b1 =>
                        {
                            b1.Property<int>("ProductId")
                                .HasColumnType("integer");

                            b1.Property<int>("Count")
                                .HasColumnType("integer")
                                .HasColumnName("RatingCount");

                            b1.Property<decimal>("Rate")
                                .HasColumnType("numeric")
                                .HasColumnName("RatingRate");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Price")
                        .IsRequired();

                    b.Navigation("Rating")
                        .IsRequired();
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.User", b =>
                {
                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.AddressValue", "Address", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("City");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("character varying(10)")
                                .HasColumnName("Number");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasMaxLength(2)
                                .HasColumnType("character varying(2)")
                                .HasColumnName("State");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("Street");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("ZipCode");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.GeoLocationValue", "GeoLocation", b2 =>
                                {
                                    b2.Property<Guid>("AddressValueUserId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Latitude")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)")
                                        .HasColumnName("Latitude");

                                    b2.Property<string>("Longitude")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)")
                                        .HasColumnName("Longitude");

                                    b2.HasKey("AddressValueUserId");

                                    b2.ToTable("Users");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressValueUserId");
                                });

                            b1.Navigation("GeoLocation")
                                .IsRequired();
                        });

                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.PersonNameValue", "Name", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("LastName");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Product", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.User", b =>
                {
                    b.Navigation("Carts");
                });
#pragma warning restore 612, 618
        }
    }
}
