using SimuladorCaixa.Aplicacao.Contratos.Persistencia;
using SimuladorCaixa.Aplicacao.DTOS.Simulacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Aplicacao.CasosDeUso.Simulacoes.ObterResumoSimulacoesPorCliente
{
    public sealed class ObterResumoSimulacoesPorClienteManipulador
    {
        private readonly IRepositorioSimulacoes _repositorioSimulacoes;

        public ObterResumoSimulacoesPorClienteManipulador(IRepositorioSimulacoes repositorioSimulacoes)
        {
            _repositorioSimulacoes = repositorioSimulacoes;
        }

        public async Task<ResumoSimulacoesClienteResposta> ExecutarAsync(
            ObterResumoSimulacoesPorClienteConsulta consulta,
            CancellationToken ct)
        {
            var simulacoes = await _repositorioSimulacoes.ListarPorClienteAsync(consulta.ClienteId, ct);

            var total = simulacoes.Count;

            if (total == 0)
            {
                return new ResumoSimulacoesClienteResposta(
                    ClienteId: consulta.ClienteId,
                    TotalSimulacoes: 0,
                    ValorTotalInvestido: 0m,
                    ValorTotalProjetado: 0m,
                    GanhoTotal: 0m,
                    RentabilidadeMediaPercentual: 0m
                );
            }

            var totalInvestido = simulacoes.Sum(s => s.Valor);
            var totalProjetado = simulacoes.Sum(s => s.ValorFinal);
            var ganhoTotal = totalProjetado - totalInvestido;

            // Média da rentabilidade percentual por simulação
            var rentMedia = simulacoes.Average(s => s.Valor == 0m ? 0m : ((s.ValorFinal - s.Valor) / s.Valor) * 100m);

            return new ResumoSimulacoesClienteResposta(
                ClienteId: consulta.ClienteId,
                TotalSimulacoes: total,
                ValorTotalInvestido: totalInvestido,
                ValorTotalProjetado: totalProjetado,
                GanhoTotal: ganhoTotal,
                RentabilidadeMediaPercentual: (decimal)rentMedia
            );
        }
    }
}
