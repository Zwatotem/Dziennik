using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dziennik.Migrations
{
    /// <inheritdoc />
    public partial class CicletoCycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_DidacticCicles_DidacticCycleId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DidacticCicles",
                table: "DidacticCicles");

            migrationBuilder.RenameTable(
                name: "DidacticCicles",
                newName: "DidacticCycles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DidacticCycles",
                table: "DidacticCycles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_DidacticCycles_DidacticCycleId",
                table: "Groups",
                column: "DidacticCycleId",
                principalTable: "DidacticCycles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_DidacticCycles_DidacticCycleId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DidacticCycles",
                table: "DidacticCycles");

            migrationBuilder.RenameTable(
                name: "DidacticCycles",
                newName: "DidacticCicles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DidacticCicles",
                table: "DidacticCicles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_DidacticCicles_DidacticCycleId",
                table: "Groups",
                column: "DidacticCycleId",
                principalTable: "DidacticCicles",
                principalColumn: "Id");
        }
    }
}
