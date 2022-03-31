﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mortoff.Persistence;

#nullable disable

namespace Mortoff.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220331165752_AddInitialTables")]
    partial class AddInitialTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mortoff.Domain.Entities.DataEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<decimal>("Close")
                        .HasColumnType("DECIMAL(11,6)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("DATE");

                    b.Property<decimal>("High")
                        .HasColumnType("DECIMAL(11,6)");

                    b.Property<decimal>("Low")
                        .HasColumnType("DECIMAL(11,6)");

                    b.Property<decimal>("Open")
                        .HasColumnType("DECIMAL(11,6)");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<long>("Volume")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("Datas", (string)null);
                });

            modelBuilder.Entity("Mortoff.Domain.Entities.StockEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Stocks", (string)null);
                });

            modelBuilder.Entity("Mortoff.Domain.Entities.DataEntity", b =>
                {
                    b.HasOne("Mortoff.Domain.Entities.StockEntity", "Stock")
                        .WithMany("Datas")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Mortoff.Domain.Entities.StockEntity", b =>
                {
                    b.Navigation("Datas");
                });
#pragma warning restore 612, 618
        }
    }
}