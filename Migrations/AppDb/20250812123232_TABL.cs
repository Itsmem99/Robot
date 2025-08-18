using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Robot.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class TABL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mood",
                table: "MoodEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mood",
                table: "MoodEntries");
        }
    }
}
