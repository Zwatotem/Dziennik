﻿// <auto-generated />
using System;
using Dziennik.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dziennik.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240115122858_Define Admin as subtype of Role explicitly in database")]
    partial class DefineAdminassubtypeofRoleexplicitlyindatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseProgramTeacher", b =>
                {
                    b.Property<Guid>("CourseProgramsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DedicatedTeachersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CourseProgramsId", "DedicatedTeachersId");

                    b.HasIndex("DedicatedTeachersId");

                    b.ToTable("CourseProgramTeacher");
                });

            modelBuilder.Entity("Dziennik.Models.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DetailedPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DidacticCycleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProgramId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RecurringPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DetailedPlanId");

                    b.HasIndex("DidacticCycleId");

                    b.HasIndex("GroupId");

                    b.HasIndex("ProgramId");

                    b.HasIndex("RecurringPlanId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Dziennik.Models.CourseProgram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DidacticLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CoursePrograms");
                });

            modelBuilder.Entity("Dziennik.Models.DidacticCycle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("DidacticCycles");
                });

            modelBuilder.Entity("Dziennik.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DidacticCycleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DidacticLevel")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("SupervisorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DidacticCycleId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SupervisorId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Dziennik.Models.RecurringPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RecurringPlans");
                });

            modelBuilder.Entity("Dziennik.Models.RoleRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DecisionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RequestedRoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RequestedRoleId");

                    b.HasIndex("UserId");

                    b.ToTable("RoleRequests");
                });

            modelBuilder.Entity("Dziennik.Models.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("IndividualRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Role");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Dziennik.Models.Scheduling.DetailedPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("DetailedPlans");
                });

            modelBuilder.Entity("Dziennik.Models.Scheduling.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DetailedPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DetailedPlanId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Dziennik.Models.Scheduling.RecurringEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time");

                    b.Property<Guid?>("RecurringPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("RecurringPlanId");

                    b.ToTable("RecurringEvents");
                });

            modelBuilder.Entity("Dziennik.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Names")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PESEL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("acbf4c1d-78c4-4e39-ad6c-faf4483e86d3"),
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("554c8032-faed-41d5-acde-1ac0f137b91a"),
                            Name = "Teacher",
                            NormalizedName = "TEACHER"
                        },
                        new
                        {
                            Id = new Guid("0423dd4b-d53f-4ca0-8728-91ea982244b5"),
                            Name = "Student",
                            NormalizedName = "STUDENT"
                        },
                        new
                        {
                            Id = new Guid("c1c73fcf-07fe-4df0-96e3-7272acfda5df"),
                            Name = "Parent",
                            NormalizedName = "PARENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Dziennik.Models.Roles.Admin", b =>
                {
                    b.HasBaseType("Dziennik.Models.Roles.Role");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Dziennik.Models.Roles.Student", b =>
                {
                    b.HasBaseType("Dziennik.Models.Roles.Role");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("GroupId");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Dziennik.Models.Roles.Teacher", b =>
                {
                    b.HasBaseType("Dziennik.Models.Roles.Role");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("CourseProgramTeacher", b =>
                {
                    b.HasOne("Dziennik.Models.CourseProgram", null)
                        .WithMany()
                        .HasForeignKey("CourseProgramsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dziennik.Models.Roles.Teacher", null)
                        .WithMany()
                        .HasForeignKey("DedicatedTeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dziennik.Models.Course", b =>
                {
                    b.HasOne("Dziennik.Models.Scheduling.DetailedPlan", "DetailedPlan")
                        .WithMany()
                        .HasForeignKey("DetailedPlanId");

                    b.HasOne("Dziennik.Models.DidacticCycle", "DidacticCycle")
                        .WithMany("Courses")
                        .HasForeignKey("DidacticCycleId");

                    b.HasOne("Dziennik.Models.Group", "Group")
                        .WithMany("Courses")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Dziennik.Models.CourseProgram", "Program")
                        .WithMany("Courses")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dziennik.Models.RecurringPlan", "RecurringPlan")
                        .WithMany()
                        .HasForeignKey("RecurringPlanId");

                    b.HasOne("Dziennik.Models.Roles.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("DetailedPlan");

                    b.Navigation("DidacticCycle");

                    b.Navigation("Group");

                    b.Navigation("Program");

                    b.Navigation("RecurringPlan");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Dziennik.Models.Group", b =>
                {
                    b.HasOne("Dziennik.Models.DidacticCycle", "DidacticCycle")
                        .WithMany("Groups")
                        .HasForeignKey("DidacticCycleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Dziennik.Models.Roles.Teacher", "Supervisor")
                        .WithMany("Groups")
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("DidacticCycle");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("Dziennik.Models.RoleRequest", b =>
                {
                    b.HasOne("Dziennik.Models.Roles.Role", "RequestedRole")
                        .WithMany()
                        .HasForeignKey("RequestedRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dziennik.Models.User", "User")
                        .WithMany("RoleRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RequestedRole");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dziennik.Models.Roles.Role", b =>
                {
                    b.HasOne("Dziennik.Models.User", "Owner")
                        .WithMany("Roles")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Dziennik.Models.Scheduling.Event", b =>
                {
                    b.HasOne("Dziennik.Models.Scheduling.DetailedPlan", null)
                        .WithMany("Events")
                        .HasForeignKey("DetailedPlanId");
                });

            modelBuilder.Entity("Dziennik.Models.Scheduling.RecurringEvent", b =>
                {
                    b.HasOne("Dziennik.Models.RecurringPlan", null)
                        .WithMany("Events")
                        .HasForeignKey("RecurringPlanId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Dziennik.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Dziennik.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dziennik.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Dziennik.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dziennik.Models.Roles.Student", b =>
                {
                    b.HasOne("Dziennik.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Dziennik.Models.CourseProgram", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Dziennik.Models.DidacticCycle", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Dziennik.Models.Group", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Dziennik.Models.RecurringPlan", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("Dziennik.Models.Scheduling.DetailedPlan", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("Dziennik.Models.User", b =>
                {
                    b.Navigation("RoleRequests");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Dziennik.Models.Roles.Teacher", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
