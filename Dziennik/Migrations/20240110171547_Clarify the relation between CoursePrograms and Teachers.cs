using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dziennik.Migrations
{
    /// <inheritdoc />
    public partial class ClarifytherelationbetweenCourseProgramsandTeachers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualRoles_CoursePrograms_CourseProgramId",
                table: "IndividualRoles");

            migrationBuilder.DropIndex(
                name: "IX_IndividualRoles_CourseProgramId",
                table: "IndividualRoles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("05b3d8ed-2358-476f-bd86-c15870341d9a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2a322a57-1393-4865-b376-7202f048ccfb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2e23d065-38bf-4a7a-a29e-7672673683fa"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5ea6d9a6-9f7d-4502-bf16-8ac4780012f8"));

            migrationBuilder.DropColumn(
                name: "CourseProgramId",
                table: "IndividualRoles");

            migrationBuilder.CreateTable(
                name: "CourseProgramTeacher",
                columns: table => new
                {
                    CourseProgramsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DedicatedTeachersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseProgramTeacher", x => new { x.CourseProgramsId, x.DedicatedTeachersId });
                    table.ForeignKey(
                        name: "FK_CourseProgramTeacher_CoursePrograms_CourseProgramsId",
                        column: x => x.CourseProgramsId,
                        principalTable: "CoursePrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseProgramTeacher_IndividualRoles_DedicatedTeachersId",
                        column: x => x.DedicatedTeachersId,
                        principalTable: "IndividualRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("76980bbe-934b-487e-a6ae-fb6caf5e45d7"), "1b1ef4a0-f837-4cab-b3da-3df8106fc66a", "Administrator", "ADMINISTRATOR" },
                    { new Guid("cdf0189f-ab0b-4dcc-bfa6-694af0c3f8db"), "f02d8310-d959-419c-aa53-7ad895e766a9", "Teacher", "TEACHER" },
                    { new Guid("e4389744-26c4-4a3c-b01f-ac96616b522e"), "fc56ea2a-d6c6-47fa-9f84-81e53ec1ae11", "Student", "STUDENT" },
                    { new Guid("f4b660f8-f806-4570-8c31-03877caab81d"), "b8ff7a29-f097-47ff-a421-9ad82b8d39b5", "Parent", "PARENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseProgramTeacher_DedicatedTeachersId",
                table: "CourseProgramTeacher",
                column: "DedicatedTeachersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseProgramTeacher");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("76980bbe-934b-487e-a6ae-fb6caf5e45d7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cdf0189f-ab0b-4dcc-bfa6-694af0c3f8db"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e4389744-26c4-4a3c-b01f-ac96616b522e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f4b660f8-f806-4570-8c31-03877caab81d"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourseProgramId",
                table: "IndividualRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("05b3d8ed-2358-476f-bd86-c15870341d9a"), "68f80428-3734-4a2f-b037-a5ca5b8ca748", "Teacher", "TEACHER" },
                    { new Guid("2a322a57-1393-4865-b376-7202f048ccfb"), "cf4a8727-82b3-4a5d-8488-30d0979d2b67", "Administrator", "ADMINISTRATOR" },
                    { new Guid("2e23d065-38bf-4a7a-a29e-7672673683fa"), "1afc9db3-1652-4491-b73b-4e6d159f0bb3", "Parent", "PARENT" },
                    { new Guid("5ea6d9a6-9f7d-4502-bf16-8ac4780012f8"), "29ceb763-2a2b-4b92-9182-23c28202c467", "Student", "STUDENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualRoles_CourseProgramId",
                table: "IndividualRoles",
                column: "CourseProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualRoles_CoursePrograms_CourseProgramId",
                table: "IndividualRoles",
                column: "CourseProgramId",
                principalTable: "CoursePrograms",
                principalColumn: "Id");
        }
    }
}
