using SimuladorCaixa.Aplicacao.Contratos.Persistencia;
using SimuladorCaixa.Dominio.Entidades;
using SimuladorCaixa.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Testes.Aplicacao.Fakes
{
    public class FakeRepositorioProdutos : IRepositorioProdutos
    {
        private readonly IReadOnlyList<ProdutoInvestimento> _produtos;

        public FakeRepositorioProdutos(IReadOnlyList<ProdutoInvestimento> produtos)
        {
            _produtos = produtos;
        }

        public Task<IReadOnlyList<ProdutoInvestimento>> ListarPorTipoAsync(
            TipoProduto tipoProduto,
            CancellationToken ct)
        {
            var resultado = _produtos.Where(p => p.TipoProduto == tipoProduto).ToList();
            return Task.FromResult<IReadOnlyList<ProdutoInvestimento>>(resultado);
        }
    }
}
