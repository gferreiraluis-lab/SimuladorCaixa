using SimuladorCaixa.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Aplicacao.Contratos.Persistencia
{
    public interface IRepositorioSimulacoes
    {
        Task AdicionarAsync(SimulacaoInvestimento simulacao, CancellationToken ct);
        Task<IReadOnlyList<SimulacaoInvestimento>> ListarPorClienteAsync(long clienteId, CancellationToken ct);
    }
}
