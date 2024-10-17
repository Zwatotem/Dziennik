using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dziennik.Migrations
{
    /// <inheritdoc />
    public partial class AddmodelsforCoursesandScheduling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseProgramId",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoursePrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DidacticLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetailedPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecurringPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailedPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_DetailedPlans_DetailedPlanId",
                        column: x => x.DetailedPlanId,
                        principalTable: "DetailedPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DidacticCycleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecurringPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DetailedPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_AspNetRoles_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_CoursePrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "CoursePrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_DetailedPlans_DetailedPlanId",
                        column: x => x.DetailedPlanId,
                        principalTable: "DetailedPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_DidacticCycles_DidacticCycleId",
                        column: x => x.DidacticCycleId,
                        principalTable: "DidacticCycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_RecurringPlans_RecurringPlanId",
                        column: x => x.RecurringPlanId,
                        principalTable: "RecurringPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecurringEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    RecurringPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringEvents_RecurringPlans_RecurringPlanId",
                        column: x => x.RecurringPlanId,
                        principalTable: "RecurringPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_CourseProgramId",
                table: "AspNetRoles",
                column: "CourseProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DetailedPlanId",
                table: "Courses",
                column: "DetailedPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DidacticCycleId",
                table: "Courses",
                column: "DidacticCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_GroupId",
                table: "Courses",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProgramId",
                table: "Courses",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_RecurringPlanId",
                table: "Courses",
                column: "RecurringPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_DetailedPlanId",
                table: "Events",
                column: "DetailedPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringEvents_RecurringPlanId",
                table: "RecurringEvents",
                column: "RecurringPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_CoursePrograms_CourseProgramId",
                table: "AspNetRoles",
                column: "CourseProgramId",
                principalTable: "CoursePrograms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_CoursePrograms_CourseProgramId",
                table: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "RecurringEvents");

            migrationBuilder.DropTable(
                name: "CoursePrograms");

            migrationBuilder.DropTable(
                name: "DetailedPlans");

            migrationBuilder.DropTable(
                name: "RecurringPlans");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_CourseProgramId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CourseProgramId",
                table: "AspNetRoles");
        }
    }
}
