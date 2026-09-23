using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Čtenářský_deník.Migrations
{
    /// <inheritdoc />
    public partial class addKnihaImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KnihaImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnihaImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnihaImages_Knihy_BookId",
                        column: x => x.BookId,
                        principalTable: "Knihy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KnihaImages_BookId",
                table: "KnihaImages",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KnihaImages");
        }
    }
}
