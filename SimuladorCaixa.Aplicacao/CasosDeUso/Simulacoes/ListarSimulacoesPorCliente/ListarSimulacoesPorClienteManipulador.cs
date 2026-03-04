using SimuladorCaixa.Aplicacao.Contratos.Persistencia;
using SimuladorCaixa.Aplicacao.DTOS.Simulacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Aplicacao.CasosDeUso.Simulacoes.ListarSimulacoesPorCliente
{
    public sealed class ListarSimulacoesPorClienteManipulador
    {
        private readonly IRepositorioSimulacoes _repositorioSimulacoes;

        public ListarSimulacoesPorClienteManipulador(IRepositorioSimulacoes repositorioSimulacoes)
        {
            _repositorioSimulacoes = repositorioSimulacoes;
        }

        public async Task<IReadOnlyList<ItemHistoricoSimulacaoResposta>> ExecutarAsync(
            ListarSimulacoesPorClienteConsulta consulta,
            CancellationToken ct)
        {
            var simulacoes = await _repositorioSimulacoes.ListarPorClienteAsync(consulta.ClienteId, ct);

            return simulacoes.Select(s => new ItemHistoricoSimulacaoResposta(
                SimulacaoId: s.Id,
                ProdutoId: s.ProdutoId,
                NomeProduto: s.Produto?.Nome ?? "Produto não carregado",
                TipoProduto: s.TipoProduto,
                RentabilidadeAnual: s.Produto?.RentabilidadeAnual ?? 0m,
                PrazoMeses: s.PrazoMeses,
                Valor: s.Valor,
                ValorFinal: s.ValorFinal,
                DataSimulacaoUtc: s.DataSimulacaoUtc
            )).ToList();
        }
    }
}
