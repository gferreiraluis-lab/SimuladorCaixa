using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::SimuladorCaixa.Dominio.Entidades;
using global::SimuladorCaixa.Dominio.Enums;
using SimuladorCaixa.Dominio.Entidades;
using SimuladorCaixa.Dominio.Enums;
using Microsoft.EntityFrameworkCore;


namespace SimuladorCaixa.Infraestrutura.Persistencia.Seed
{

    public static class InicializadorBanco
    {
        public static async Task InicializarAsync(SimuladorCaixaDbContext contexto, CancellationToken ct)
        {
            
            await contexto.Database.MigrateAsync(ct);

           
            if (await contexto.ProdutosInvestimento.AnyAsync(ct))
                return;

            var produtos = new List<ProdutoInvestimento>
        {
            new ProdutoInvestimento(
                nome: "CDB Caixa Conservador",
                tipoProduto: TipoProduto.Cdb,
                rentabilidadeAnual: 0.10m,
                risco: NivelRisco.Baixo,
                prazoMinimoMeses: 3,
                prazoMaximoMeses: 24,
                valorMinimo: 1000m,
                valorMaximo: 100000m),

            new ProdutoInvestimento(
                nome: "CDB Caixa Premium",
                tipoProduto: TipoProduto.Cdb,
                rentabilidadeAnual: 0.12m,
                risco: NivelRisco.Medio,
                prazoMinimoMeses: 6,
                prazoMaximoMeses: 36,
                valorMinimo: 5000m,
                valorMaximo: 500000m),

            new ProdutoInvestimento(
                nome: "LCI Caixa",
                tipoProduto: TipoProduto.Lci,
                rentabilidadeAnual: 0.095m,
                risco: NivelRisco.Baixo,
                prazoMinimoMeses: 6,
                prazoMaximoMeses: 36,
                valorMinimo: 2000m,
                valorMaximo: 300000m),

            new ProdutoInvestimento(
                nome: "LCA Caixa Agro",
                tipoProduto: TipoProduto.Lca,
                rentabilidadeAnual: 0.098m,
                risco: NivelRisco.Baixo,
                prazoMinimoMeses: 6,
                prazoMaximoMeses: 48,
                valorMinimo: 2000m,
                valorMaximo: 400000m),

            new ProdutoInvestimento(
                nome: "Tesouro Selic Simulado",
                tipoProduto: TipoProduto.Tesouro,
                rentabilidadeAnual: 0.11m,
                risco: NivelRisco.Baixo,
                prazoMinimoMeses: 1,
                prazoMaximoMeses: 60,
                valorMinimo: 100m,
                valorMaximo: 1000000m),
        };

            await contexto.ProdutosInvestimento.AddRangeAsync(produtos, ct);
            await contexto.SaveChangesAsync(ct);
        }
    }
}
