using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Čtenářský_deník.Migrations
{
    /// <inheritdoc />
    public partial class AddZanrToKniha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Knihy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zanr",
                table: "Knihy",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zanr",
                table: "Knihy");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Knihy",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
