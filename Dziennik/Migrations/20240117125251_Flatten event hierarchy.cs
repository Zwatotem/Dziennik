using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dziennik.Migrations
{
    /// <inheritdoc />
    public partial class Flatteneventhierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "RecurringEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "RecurringEvents");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Events",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }
    }
}
