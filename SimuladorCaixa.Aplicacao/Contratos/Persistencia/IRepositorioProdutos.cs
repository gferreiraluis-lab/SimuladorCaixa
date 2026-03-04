using SimuladorCaixa.Dominio.Entidades;
using SimuladorCaixa.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Aplicacao.Contratos.Persistencia
{
    public interface IRepositorioProdutos
    {
        Task<IReadOnlyList<ProdutoInvestimento>> ListarPorTipoAsync(TipoProduto tipoProduto, CancellationToken ct);
    }
}
