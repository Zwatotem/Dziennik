using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dziennik.Migrations
{
    /// <inheritdoc />
    public partial class StabilizeseedingofASPRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0423dd4b-d53f-4ca0-8728-91ea982244b5"), null, "Student", "STUDENT" },
                    { new Guid("554c8032-faed-41d5-acde-1ac0f137b91a"), null, "Teacher", "TEACHER" },
                    { new Guid("acbf4c1d-78c4-4e39-ad6c-faf4483e86d3"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("c1c73fcf-07fe-4df0-96e3-7272acfda5df"), null, "Parent", "PARENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0423dd4b-d53f-4ca0-8728-91ea982244b5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("554c8032-faed-41d5-acde-1ac0f137b91a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("acbf4c1d-78c4-4e39-ad6c-faf4483e86d3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c1c73fcf-07fe-4df0-96e3-7272acfda5df"));

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
        }
    }
}
