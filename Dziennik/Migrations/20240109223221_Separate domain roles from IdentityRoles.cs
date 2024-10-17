using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dziennik.Migrations
{
    /// <inheritdoc />
    public partial class SeparatedomainrolesfromIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_OwnerId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_CoursePrograms_CourseProgramId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Groups_GroupId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetRoles_TeacherId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetRoles_SupervisorId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleRequests_AspNetRoles_RequestedRoleId",
                table: "RoleRequests");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_CourseProgramId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_GroupId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_OwnerId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CourseProgramId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "IndividualRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualRoles_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualRoles_CoursePrograms_CourseProgramId",
                        column: x => x.CourseProgramId,
                        principalTable: "CoursePrograms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IndividualRoles_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_IndividualRoles_CourseProgramId",
                table: "IndividualRoles",
                column: "CourseProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualRoles_GroupId",
                table: "IndividualRoles",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualRoles_OwnerId",
                table: "IndividualRoles",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_IndividualRoles_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "IndividualRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_IndividualRoles_SupervisorId",
                table: "Groups",
                column: "SupervisorId",
                principalTable: "IndividualRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleRequests_IndividualRoles_RequestedRoleId",
                table: "RoleRequests",
                column: "RequestedRoleId",
                principalTable: "IndividualRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_IndividualRoles_TeacherId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_IndividualRoles_SupervisorId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleRequests_IndividualRoles_RequestedRoleId",
                table: "RoleRequests");

            migrationBuilder.DropTable(
                name: "IndividualRoles");

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

            migrationBuilder.AddColumn<Guid>(
                name: "CourseProgramId",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_CourseProgramId",
                table: "AspNetRoles",
                column: "CourseProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_GroupId",
                table: "AspNetRoles",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_OwnerId",
                table: "AspNetRoles",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_OwnerId",
                table: "AspNetRoles",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_CoursePrograms_CourseProgramId",
                table: "AspNetRoles",
                column: "CourseProgramId",
                principalTable: "CoursePrograms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Groups_GroupId",
                table: "AspNetRoles",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetRoles_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetRoles_SupervisorId",
                table: "Groups",
                column: "SupervisorId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleRequests_AspNetRoles_RequestedRoleId",
                table: "RoleRequests",
                column: "RequestedRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
