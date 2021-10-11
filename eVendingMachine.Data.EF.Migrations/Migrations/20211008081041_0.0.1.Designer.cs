﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eVendingMachine.Data.EF;

namespace eVendingMachine.Data.EF.Migrations.Migrations
{
    [DbContext(typeof(VendingMachineDbContext))]
    [Migration("20211008081041_0.0.1")]
    partial class _001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eVendingMachine.Domain.Cash", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CashType")
                        .HasColumnType("int");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Cashes");
                });

            modelBuilder.Entity("eVendingMachine.Domain.CashIO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CashId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("InOut")
                        .HasColumnType("int");

                    b.Property<Guid>("PurchaseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CashId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("CashIOs");
                });

            modelBuilder.Entity("eVendingMachine.Domain.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("eVendingMachine.Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("eVendingMachine.Domain.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalInCash")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalOutCash")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("eVendingMachine.Domain.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CashId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CashId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("eVendingMachine.Domain.Cash", b =>
                {
                    b.HasOne("eVendingMachine.Domain.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("eVendingMachine.Domain.CashIO", b =>
                {
                    b.HasOne("eVendingMachine.Domain.Cash", "Cash")
                        .WithMany("CashesIO")
                        .HasForeignKey("CashId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eVendingMachine.Domain.Purchase", "Purchase")
                        .WithMany("CashesIO")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cash");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("eVendingMachine.Domain.Purchase", b =>
                {
                    b.HasOne("eVendingMachine.Domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("eVendingMachine.Domain.Wallet", b =>
                {
                    b.HasOne("eVendingMachine.Domain.Cash", "Cash")
                        .WithMany()
                        .HasForeignKey("CashId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cash");
                });

            modelBuilder.Entity("eVendingMachine.Domain.Cash", b =>
                {
                    b.Navigation("CashesIO");
                });

            modelBuilder.Entity("eVendingMachine.Domain.Purchase", b =>
                {
                    b.Navigation("CashesIO");
                });
#pragma warning restore 612, 618
        }
    }
}