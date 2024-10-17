using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dziennik.Migrations
{
    /// <inheritdoc />
    public partial class Addalldetailstoseededidentityroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3cdf20b9-7c70-425d-accb-41371e79a553"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("43650c2a-4507-414d-bf6a-e7b4e7db97ad"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("721b8b03-4a04-417d-b0c5-468cce6e8ce9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4975613-ffc2-4167-9d49-fb71ef0eb35c"));

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3cdf20b9-7c70-425d-accb-41371e79a553"), null, "Parent", null },
                    { new Guid("43650c2a-4507-414d-bf6a-e7b4e7db97ad"), null, "Administrator", null },
                    { new Guid("721b8b03-4a04-417d-b0c5-468cce6e8ce9"), null, "Teacher", null },
                    { new Guid("d4975613-ffc2-4167-9d49-fb71ef0eb35c"), null, "Student", null }
                });
        }
    }
}
