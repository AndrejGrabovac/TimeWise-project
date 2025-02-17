﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeWise.Data;

#nullable disable

namespace TimeWise.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250131222114_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TimeWise.Models.WorkHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<TimeSpan>("HourFrom")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HourTo")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HoursWorked")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("WorkHours");
                });
#pragma warning restore 612, 618
        }
    }
}
