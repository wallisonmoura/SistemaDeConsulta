using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeConsulta.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Exames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Exames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TipoExameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exames_Tipo_Exames_TipoExameId",
                        column: x => x.TipoExameId,
                        principalTable: "Tipo_Exames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacientId = table.Column<int>(type: "int", nullable: false),
                    ExameId = table.Column<int>(type: "int", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroProtocolo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultas_Exames_ExameId",
                        column: x => x.ExameId,
                        principalTable: "Exames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultas_Pacientes_PacientId",
                        column: x => x.PacientId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_ExameId",
                table: "Consultas",
                column: "ExameId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PacientId",
                table: "Consultas",
                column: "PacientId");

            migrationBuilder.CreateIndex(
                name: "IX_Exames_TipoExameId",
                table: "Exames",
                column: "TipoExameId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_CPF",
                table: "Pacientes",
                column: "CPF",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Exames");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Tipo_Exames");
        }
    }
}
