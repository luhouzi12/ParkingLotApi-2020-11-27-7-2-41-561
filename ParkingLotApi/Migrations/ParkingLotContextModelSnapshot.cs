﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Migrations
{
    [DbContext(typeof(ParkingLotContext))]
    partial class ParkingLotContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ParkingLotApi.Entities.OrderEntity", b =>
                {
                    b.Property<string>("OrderNumber")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CloseTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ParkingLotEntityName")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ParkingLotName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("OrderNumber");

                    b.HasIndex("ParkingLotEntityName");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ParkingLotApi.Entities.ParkingLotEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Name");

                    b.ToTable("ParkingLots");
                });

            modelBuilder.Entity("ParkingLotApi.Entities.OrderEntity", b =>
                {
                    b.HasOne("ParkingLotApi.Entities.ParkingLotEntity", null)
                        .WithMany("Orders")
                        .HasForeignKey("ParkingLotEntityName");
                });
#pragma warning restore 612, 618
        }
    }
}
