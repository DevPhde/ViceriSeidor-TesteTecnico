using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperHeroes.Infra.Data.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Herois",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 120, nullable: false),
                    NomeHeroi = table.Column<string>(maxLength: 120, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    Altura = table.Column<double>(type: "float", nullable: false),
                    Peso = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Herois", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Superpoderes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Superpoder = table.Column<string>(maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Superpoderes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeroisSuperpoderes",
                columns: table => new
                {
                    HeroiId = table.Column<int>(nullable: false),
                    SuperpoderId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroisSuperpoderes", x => new { x.HeroiId, x.SuperpoderId });
                    table.ForeignKey(
                        name: "FK_HeroisSuperpoderes_Herois_HeroiId",
                        column: x => x.HeroiId,
                        principalTable: "Herois",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroisSuperpoderes_Superpoderes_SuperpoderId",
                        column: x => x.SuperpoderId,
                        principalTable: "Superpoderes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroisSuperpoderes_SuperpoderId",
                table: "HeroisSuperpoderes",
                column: "SuperpoderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroisSuperpoderes");

            migrationBuilder.DropTable(
                name: "Herois");

            migrationBuilder.DropTable(
                name: "Superpoderes");
        }
    }
}
