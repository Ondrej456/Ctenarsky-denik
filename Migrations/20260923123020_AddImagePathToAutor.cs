using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Čtenářský_deník.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToAutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Autori",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Autori");
        }
    }
}
