using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Robot.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class MOODUpd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoodEntries_AspNetUsers_UserId",
                table: "MoodEntries");

            migrationBuilder.DropIndex(
                name: "IX_MoodEntries_UserId",
                table: "MoodEntries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MoodEntries");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Usermail",
                table: "MoodEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usermail",
                table: "MoodEntries");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MoodEntries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoodEntries_UserId",
                table: "MoodEntries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoodEntries_AspNetUsers_UserId",
                table: "MoodEntries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
