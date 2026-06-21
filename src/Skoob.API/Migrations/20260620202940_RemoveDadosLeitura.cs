using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Skoob.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDadosLeitura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosLeituras");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "EstantesLivros");

            migrationBuilder.DropColumn(
                name: "DataTermino",
                table: "EstantesLivros");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "EstantesLivros",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataTermino",
                table: "EstantesLivros",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistoricosLeituras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comentario = table.Column<string>(type: "text", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstanteLivroId = table.Column<int>(type: "integer", nullable: false),
                    PaginaLida = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosLeituras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosLeituras_EstantesLivros_EstanteLivroId",
                        column: x => x.EstanteLivroId,
                        principalTable: "EstantesLivros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosLeituras_EstanteLivroId",
                table: "HistoricosLeituras",
                column: "EstanteLivroId");
        }
    }
}
