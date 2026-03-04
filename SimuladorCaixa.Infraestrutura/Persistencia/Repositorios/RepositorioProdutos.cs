using Microsoft.EntityFrameworkCore;
using SimuladorCaixa.Aplicacao.Contratos.Persistencia;
using SimuladorCaixa.Dominio.Entidades;
using SimuladorCaixa.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Infraestrutura.Persistencia.Repositorios
{
    public sealed class RepositorioProdutos : IRepositorioProdutos
    {
        private readonly SimuladorCaixaDbContext _contexto;

        public RepositorioProdutos(SimuladorCaixaDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IReadOnlyList<ProdutoInvestimento>> ListarPorTipoAsync(
            TipoProduto tipoProduto,
            CancellationToken ct)
        {
            return await _contexto.ProdutosInvestimento
                .Where(p => p.TipoProduto == tipoProduto)
                .ToListAsync(ct);
        }
    }
}
