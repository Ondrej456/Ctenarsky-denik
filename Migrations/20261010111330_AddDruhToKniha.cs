using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Čtenářský_deník.Migrations
{
    /// <inheritdoc />
    public partial class AddDruhToKniha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Druh",
                table: "Knihy",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Druh",
                table: "Knihy");
        }
    }
}
