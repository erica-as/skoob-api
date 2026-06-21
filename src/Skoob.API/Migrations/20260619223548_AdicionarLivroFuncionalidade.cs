using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skoob.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarLivroFuncionalidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isbn",
                table: "Livros");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Isbn",
                table: "Livros",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
