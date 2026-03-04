using SimuladorCaixa.Aplicacao.Contratos.Persistencia;
using SimuladorCaixa.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Testes.Aplicacao.Fakes
{
    public class FakeRepositorioSimulacoes : IRepositorioSimulacoes
    {
        public Task AdicionarAsync(SimulacaoInvestimento simulacao, CancellationToken ct)
        {
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<SimulacaoInvestimento>> ListarPorClienteAsync(
            long clienteId,
            CancellationToken ct)
        {
            return Task.FromResult<IReadOnlyList<SimulacaoInvestimento>>(
                new List<SimulacaoInvestimento>());
        }
    }
}
