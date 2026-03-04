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
    public sealed class ProdutoInvestimentoMapeamento : IEntityTypeConfiguration<ProdutoInvestimento>
    {
        public void Configure(EntityTypeBuilder<ProdutoInvestimento> builder)
        {
            builder.ToTable("produtos_investimento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .HasColumnName("nome")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.TipoProduto)
                .HasColumnName("tipo_produto")
                .IsRequired();

            builder.Property(x => x.RentabilidadeAnual)
                .HasColumnName("rentabilidade_anual")
                .HasColumnType("decimal(18,6)")
                .IsRequired();

            builder.Property(x => x.Risco)
                .HasColumnName("risco")
                .IsRequired();

            builder.Property(x => x.PrazoMinimoMeses)
                .HasColumnName("prazo_minimo_meses")
                .IsRequired();

            builder.Property(x => x.PrazoMaximoMeses)
                .HasColumnName("prazo_maximo_meses")
                .IsRequired();

            builder.Property(x => x.ValorMinimo)
                .HasColumnName("valor_minimo")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.ValorMaximo)
                .HasColumnName("valor_maximo")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasIndex(x => x.TipoProduto)
                .HasDatabaseName("ix_produtos_tipo_produto");
        }
    }
}
