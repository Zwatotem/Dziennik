using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dziennik.Migrations
{
    /// <inheritdoc />
    public partial class Addgroupnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_DidacticCicles_DidacticCicleId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "DidacticCicleId",
                table: "Groups",
                newName: "DidacticCycleId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_DidacticCicleId",
                table: "Groups",
                newName: "IX_Groups_DidacticCycleId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_DidacticCicles_DidacticCycleId",
                table: "Groups",
                column: "DidacticCycleId",
                principalTable: "DidacticCicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_DidacticCicles_DidacticCycleId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "DidacticCycleId",
                table: "Groups",
                newName: "DidacticCicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_DidacticCycleId",
                table: "Groups",
                newName: "IX_Groups_DidacticCicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_DidacticCicles_DidacticCicleId",
                table: "Groups",
                column: "DidacticCicleId",
                principalTable: "DidacticCicles",
                principalColumn: "Id");
        }
    }
}
