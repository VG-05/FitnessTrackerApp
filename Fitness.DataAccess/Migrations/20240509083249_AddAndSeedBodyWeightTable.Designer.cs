﻿// <auto-generated />
using System;
using Fitness.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240509083249_AddAndSeedBodyWeightTable")]
    partial class AddAndSeedBodyWeightTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Fitness.Models.BodyWeight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BodyWeights");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Unit = 0,
                            Weight = 80
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2020, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Unit = 0,
                            Weight = 75
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2020, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Unit = 0,
                            Weight = 60
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
