﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace OrderImport.Migrations
{
    [DbContext(typeof(OrderImportContext))]
    internal class OrderImportContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("OrderImport.Customer", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .UseIdentityColumn();

                b.Property<decimal>("CreditLimit")
                    .HasColumnType("decimal(8,2)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.HasKey("Id");

                b.ToTable("Customers");
            });

            modelBuilder.Entity("OrderImport.Order", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .UseIdentityColumn();

                b.Property<int>("CustomerId")
                    .HasColumnType("int");

                b.Property<DateTime>("OrderDate")
                    .HasColumnType("datetime2");

                b.Property<decimal>("OrderValue")
                    .HasColumnType("decimal(8,2)");

                b.HasKey("Id");

                b.HasIndex("CustomerId");

                b.ToTable("Orders");
            });

            modelBuilder.Entity("OrderImport.Order", b =>
            {
                b.HasOne("OrderImport.Customer", "Customer")
                    .WithMany("Orders")
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Customer");
            });

            modelBuilder.Entity("OrderImport.Customer", b => { b.Navigation("Orders"); });
#pragma warning restore 612, 618
        }
    }
}