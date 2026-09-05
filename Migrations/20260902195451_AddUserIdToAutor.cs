using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Čtenářský_deník.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToAutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Knihy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Autori",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Knihy");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Autori");
        }
    }
}
