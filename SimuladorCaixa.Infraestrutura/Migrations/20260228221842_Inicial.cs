using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimuladorCaixa.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "produtos_investimento",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    tipo_produto = table.Column<int>(type: "INTEGER", nullable: false),
                    rentabilidade_anual = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    risco = table.Column<int>(type: "INTEGER", nullable: false),
                    prazo_minimo_meses = table.Column<int>(type: "INTEGER", nullable: false),
                    prazo_maximo_meses = table.Column<int>(type: "INTEGER", nullable: false),
                    valor_minimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    valor_maximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtos_investimento", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "simulacoes_investimento",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cliente_id = table.Column<long>(type: "INTEGER", nullable: false),
                    produto_id = table.Column<int>(type: "INTEGER", nullable: false),
                    tipo_produto = table.Column<int>(type: "INTEGER", nullable: false),
                    valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    prazo_meses = table.Column<int>(type: "INTEGER", nullable: false),
                    valor_final = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data_simulacao_utc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simulacoes_investimento", x => x.id);
                    table.ForeignKey(
                        name: "FK_simulacoes_investimento_produtos_investimento_produto_id",
                        column: x => x.produto_id,
                        principalTable: "produtos_investimento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_produtos_tipo_produto",
                table: "produtos_investimento",
                column: "tipo_produto");

            migrationBuilder.CreateIndex(
                name: "ix_simulacoes_cliente_id",
                table: "simulacoes_investimento",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "ix_simulacoes_data",
                table: "simulacoes_investimento",
                column: "data_simulacao_utc");

            migrationBuilder.CreateIndex(
                name: "IX_simulacoes_investimento_produto_id",
                table: "simulacoes_investimento",
                column: "produto_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "simulacoes_investimento");

            migrationBuilder.DropTable(
                name: "produtos_investimento");
        }
    }
}
