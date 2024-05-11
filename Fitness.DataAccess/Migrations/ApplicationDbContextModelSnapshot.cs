﻿// <auto-generated />
using System;
using Fitness.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BodyWeights");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Unit = "kgs",
                            Weight = 80
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2020, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Unit = "kgs",
                            Weight = 75
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2020, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Unit = "kgs",
                            Weight = 60
                        });
                });

            modelBuilder.Entity("Fitness.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("TargetDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TargetWeight")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Goal");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TargetDate = new DateTime(2020, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TargetWeight = 50,
                            Unit = "kgs"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
