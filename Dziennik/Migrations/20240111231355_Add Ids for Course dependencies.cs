using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dziennik.Migrations
{
    /// <inheritdoc />
    public partial class AddIdsforCoursedependencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_DidacticCycles_DidacticCycleId",
                table: "Courses");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "DidacticCycleId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5c20aaaf-f7f9-4078-8d46-945067fe06f8"), "c2a4aced-0513-47a1-ad09-02ccd1120907", "Parent", "PARENT" },
                    { new Guid("6432487e-7ace-4441-8385-59cda9329cb9"), "60df5750-fb33-43fb-a5ab-a27b4720f84e", "Teacher", "TEACHER" },
                    { new Guid("bf12f928-986e-49ed-bacc-8a1a1e4bdff8"), "6c1bfb29-d9b4-4c50-9ad7-605f2e754ac8", "Student", "STUDENT" },
                    { new Guid("bfeae1fc-7870-4aaa-baac-89e8f3eba751"), "8e4893e0-3f45-45af-b003-d4dff4dd936e", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_DidacticCycles_DidacticCycleId",
                table: "Courses",
                column: "DidacticCycleId",
                principalTable: "DidacticCycles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_DidacticCycles_DidacticCycleId",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5c20aaaf-f7f9-4078-8d46-945067fe06f8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6432487e-7ace-4441-8385-59cda9329cb9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bf12f928-986e-49ed-bacc-8a1a1e4bdff8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bfeae1fc-7870-4aaa-baac-89e8f3eba751"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DidacticCycleId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_DidacticCycles_DidacticCycleId",
                table: "Courses",
                column: "DidacticCycleId",
                principalTable: "DidacticCycles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
