﻿// <auto-generated />
using System;
using JasaDinnerClubBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JasaDinnerClubBackend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250109002940_AddDataTable")]
    partial class AddDataTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JasaDinnerClubBackend.Models.Attendee", b =>
                {
                    b.Property<int>("AttendeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendeeId"));

                    b.Property<string>("AttendeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttendeeNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AttendeeId");

                    b.ToTable("Attendee");

                    b.HasData(
                        new
                        {
                            AttendeeId = 1,
                            AttendeeName = "John Doe",
                            AttendeeNumber = "123-456-7890"
                        });
                });

            modelBuilder.Entity("JasaDinnerClubBackend.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<int>("AttendeeId")
                        .HasColumnType("int");

                    b.Property<int?>("DinnerEventDinnerId")
                        .HasColumnType("int");

                    b.Property<int>("DinnerId")
                        .HasColumnType("int");

                    b.Property<string>("Request")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookingId");

                    b.HasIndex("AttendeeId");

                    b.HasIndex("DinnerEventDinnerId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            BookingId = 1,
                            AttendeeId = 1,
                            DinnerId = 1,
                            Request = "Vegetarian meal"
                        });
                });

            modelBuilder.Entity("JasaDinnerClubBackend.Models.DinnerEvent", b =>
                {
                    b.Property<int>("DinnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DinnerId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("DinnerId");

                    b.ToTable("DinnerEvents");

                    b.HasData(
                        new
                        {
                            DinnerId = 1,
                            Capacity = 6,
                            Date = new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Exclusive wine tasting",
                            Name = "Wine Night",
                            Time = new TimeSpan(0, 19, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("JasaDinnerClubBackend.Models.Booking", b =>
                {
                    b.HasOne("JasaDinnerClubBackend.Models.Attendee", "Attendee")
                        .WithMany("Bookings")
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JasaDinnerClubBackend.Models.DinnerEvent", "DinnerEvent")
                        .WithMany("Bookings")
                        .HasForeignKey("DinnerEventDinnerId");

                    b.Navigation("Attendee");

                    b.Navigation("DinnerEvent");
                });

            modelBuilder.Entity("JasaDinnerClubBackend.Models.Attendee", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("JasaDinnerClubBackend.Models.DinnerEvent", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
