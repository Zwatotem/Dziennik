using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dziennik.Migrations
{
    /// <inheritdoc />
    public partial class Adddydacticcycles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_DidacticCicle_DidacticCicleId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DidacticCicle",
                table: "DidacticCicle");

            migrationBuilder.RenameTable(
                name: "DidacticCicle",
                newName: "DidacticCicles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DidacticCicles",
                table: "DidacticCicles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_DidacticCicles_DidacticCicleId",
                table: "Groups",
                column: "DidacticCicleId",
                principalTable: "DidacticCicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_DidacticCicles_DidacticCicleId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DidacticCicles",
                table: "DidacticCicles");

            migrationBuilder.RenameTable(
                name: "DidacticCicles",
                newName: "DidacticCicle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DidacticCicle",
                table: "DidacticCicle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_DidacticCicle_DidacticCicleId",
                table: "Groups",
                column: "DidacticCicleId",
                principalTable: "DidacticCicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
