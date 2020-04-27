using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ativo",
                columns: table => new
                {
                    IdAtivo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    descricao = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    lote_minimo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ativo", x => x.IdAtivo);
                });

            migrationBuilder.CreateTable(
                name: "Ordem",
                columns: table => new
                {
                    id_ordem = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    fk_id_ativo = table.Column<int>(nullable: false),
                    quantidade = table.Column<double>(nullable: false),
                    preco = table.Column<double>(nullable: false),
                    data = table.Column<DateTime>(type: "date", nullable: false),
                    classe_negociacao = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordem", x => x.id_ordem);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ativo");

            migrationBuilder.DropTable(
                name: "Ordem");
        }
    }
}
