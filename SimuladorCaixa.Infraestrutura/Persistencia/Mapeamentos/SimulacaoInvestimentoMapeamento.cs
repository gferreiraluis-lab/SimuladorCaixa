using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimuladorCaixa.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Infraestrutura.Persistencia.Mapeamentos
{
    public sealed class SimulacaoInvestimentoMapeamento : IEntityTypeConfiguration<SimulacaoInvestimento>
    {
        public void Configure(EntityTypeBuilder<SimulacaoInvestimento> builder)
        {
            builder.ToTable("simulacoes_investimento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ClienteId)
                .HasColumnName("cliente_id")
                .IsRequired();

            builder.Property(x => x.ProdutoId)
                .HasColumnName("produto_id")
                .IsRequired();

            builder.Property(x => x.TipoProduto)
                .HasColumnName("tipo_produto")
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasColumnName("valor")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.PrazoMeses)
                .HasColumnName("prazo_meses")
                .IsRequired();

            builder.Property(x => x.ValorFinal)
                .HasColumnName("valor_final")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.DataSimulacaoUtc)
                .HasColumnName("data_simulacao_utc")
                .IsRequired();

            // Relacionamento: uma simulação tem um produto
            builder.HasOne(x => x.Produto)
                .WithMany()
                .HasForeignKey(x => x.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índice útil para o GET /simulacoes?clienteId=
            builder.HasIndex(x => x.ClienteId)
                .HasDatabaseName("ix_simulacoes_cliente_id");

            // Índice útil para histórico ordenado por data
            builder.HasIndex(x => x.DataSimulacaoUtc)
                .HasDatabaseName("ix_simulacoes_data");
        }
    }
}
