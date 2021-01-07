﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VirturlMeetingAssitant.Backend.Db;

namespace Backend.Migrations
{
    [DbContext(typeof(MeetingContext))]
    [Migration("20210107175209_0108")]
    partial class _0108
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DepartmentMeeting", b =>
                {
                    b.Property<int>("DepartmentsID")
                        .HasColumnType("integer");

                    b.Property<int>("RelatedMeetingsID")
                        .HasColumnType("integer");

                    b.HasKey("DepartmentsID", "RelatedMeetingsID");

                    b.HasIndex("RelatedMeetingsID");

                    b.ToTable("DepartmentMeeting");
                });

            modelBuilder.Entity("MeetingUser", b =>
                {
                    b.Property<int>("AttendMeetingsID")
                        .HasColumnType("integer");

                    b.Property<int>("AttendeesID")
                        .HasColumnType("integer");

                    b.HasKey("AttendMeetingsID", "AttendeesID");

                    b.HasIndex("AttendeesID");

                    b.ToTable("MeetingUser");
                });

            modelBuilder.Entity("VirturlMeetingAssitant.Backend.Db.Department", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("LastUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("VirturlMeetingAssitant.Backend.Db.Meeting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("CreatorID")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("LastUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("LocationID")
                        .HasColumnType("integer");

                    b.Property<int>("RepeatType")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.HasIndex("CreatorID");

                    b.HasIndex("LocationID");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("VirturlMeetingAssitant.Backend.Db.OneTimePassword", b =>
                {
                    b.Property<string>("Hash")
                        .HasColumnType("text");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean");

                    b.Property<int?>("RelatedUserID")
                        .HasColumnType("integer");

                    b.HasKey("Hash");

                    b.HasIndex("RelatedUserID");

                    b.ToTable("OneTimePasswords");
                });

            modelBuilder.Entity("VirturlMeetingAssitant.Backend.Db.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("LastUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("VirturlMeetingAssitant.Backend.Db.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GroupType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsNeededChangePassword")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CreatedTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "manager@example.com",
                            GroupType = 1,
                            IsNeededChangePassword = true,
                            IsVerified = true,
                            LastUpdateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Manager",
                            Password = "$2a$11$2ZEmjgtNjkN63Bcpr3IK4Ozez51JSsI7aZmszpq/WEBHOAFiPa2Ry"
                        });
                });

            modelBuilder.Entity("DepartmentMeeting", b =>
                {
                    b.HasOne("VirturlMeetingAssitant.Backend.Db.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VirturlMeetingAssitant.Backend.Db.Meeting", null)
                        .WithMany()
                        .HasForeignKey("RelatedMeetingsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MeetingUser", b =>
                {
                    b.HasOne("VirturlMeetingAssitant.Backend.Db.Meeting", null)
                        .WithMany()
                        .HasForeignKey("AttendMeetingsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VirturlMeetingAssitant.Backend.Db.User", null)
                        .WithMany()
                        .HasForeignKey("AttendeesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VirturlMeetingAssitant.Backend.Db.Meeting", b =>
                {
                    b.HasOne("VirturlMeetingAssitant.Backend.Db.User", "Creator")
                        .WithMany("CreatedMeetings")
                        .HasForeignKey("CreatorID");

                    b.HasOne("VirturlMeetingAssitant.Backend.Db.Room", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID");

                    b.Navigation("Creator");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("VirturlMeetingAssitant.Backend.Db.OneTimePassword", b =>
                {
                    b.HasOne("VirturlMeetingAssitant.Backend.Db.User", "RelatedUser")
                        .WithMany()
                        .HasForeignKey("RelatedUserID");

                    b.Navigation("RelatedUser");
                });

            modelBuilder.Entity("VirturlMeetingAssitant.Backend.Db.User", b =>
                {
                    b.HasOne("VirturlMeetingAssitant.Backend.Db.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Department");
                });

            modelBuilder.Entity("VirturlMeetingAssitant.Backend.Db.Department", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("VirturlMeetingAssitant.Backend.Db.User", b =>
                {
                    b.Navigation("CreatedMeetings");
                });
#pragma warning restore 612, 618
        }
    }
}
