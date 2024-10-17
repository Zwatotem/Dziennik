using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dziennik.Migrations
{
    /// <inheritdoc />
    public partial class AddPresence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Events",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PresenceId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TemplateId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PresenceLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Teacher = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresenceLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PresenceRecords",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresenceLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Presence = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresenceRecords", x => new { x.StudentId, x.PresenceLogId });
                    table.ForeignKey(
                        name: "FK_PresenceRecords_IndividualRoles_StudentId",
                        column: x => x.StudentId,
                        principalTable: "IndividualRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PresenceRecords_PresenceLogs_PresenceLogId",
                        column: x => x.PresenceLogId,
                        principalTable: "PresenceLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CourseId",
                table: "Events",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_PresenceId",
                table: "Events",
                column: "PresenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TemplateId",
                table: "Events",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PresenceRecords_PresenceLogId",
                table: "PresenceRecords",
                column: "PresenceLogId");

            migrationBuilder.CreateIndex(
                name: "IX_PresenceRecords_StudentId",
                table: "PresenceRecords",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Courses_CourseId",
                table: "Events",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_PresenceLogs_PresenceId",
                table: "Events",
                column: "PresenceId",
                principalTable: "PresenceLogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_RecurringEvents_TemplateId",
                table: "Events",
                column: "TemplateId",
                principalTable: "RecurringEvents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Courses_CourseId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_PresenceLogs_PresenceId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_RecurringEvents_TemplateId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "PresenceRecords");

            migrationBuilder.DropTable(
                name: "PresenceLogs");

            migrationBuilder.DropIndex(
                name: "IX_Events_CourseId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_PresenceId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_TemplateId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PresenceId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Events");
        }
    }
}
