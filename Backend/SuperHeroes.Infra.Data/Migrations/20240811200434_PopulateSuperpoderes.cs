using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperHeroes.Infra.Data.Migrations
{
    public partial class PopulateSuperpoderes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Inserir dados na tabela Superpoderes
            migrationBuilder.InsertData(
                table: "Superpoderes",
                columns: new[] { "Superpoder", "Descricao" },
                values: new object[,]
                {
                { "Força Sobre-Humana", "Capacidade de exercer força física muito além dos limites humanos normais." },
                { "Invisibilidade", "Habilidade de se tornar invisível aos olhos dos outros." },
                { "Telepatia", "Capacidade de ler mentes e se comunicar mentalmente com outros." },
                { "Voo", "Habilidade de voar sem a necessidade de asas ou outro suporte mecânico." },
                { "Velocidade Sobrehumana", "Capacidade de se mover a velocidades muito superiores às humanas normais." }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remover os dados inseridos
            migrationBuilder.DeleteData(
                table: "Superpoderes",
                keyColumn: "Superpoder",
                keyValues: new object[]
                {
                "Força Sobre-Humana",
                "Invisibilidade",
                "Telepatia",
                "Voo",
                "Velocidade Sobrehumana"
                });
        }
    }
}
