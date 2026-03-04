using Microsoft.EntityFrameworkCore;
using SimuladorCaixa.Aplicacao.Contratos.Persistencia;
using SimuladorCaixa.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Infraestrutura.Persistencia.Repositorios
{
    public sealed class RepositorioSimulacoes : IRepositorioSimulacoes
    {
        private readonly SimuladorCaixaDbContext _contexto;

        public RepositorioSimulacoes(SimuladorCaixaDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task AdicionarAsync(SimulacaoInvestimento simulacao, CancellationToken ct)
        {
            await _contexto.SimulacoesInvestimento.AddAsync(simulacao, ct);
            await _contexto.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyList<SimulacaoInvestimento>> ListarPorClienteAsync(
            long clienteId,
            CancellationToken ct)
        {
            return await _contexto.SimulacoesInvestimento
                .Include(s => s.Produto)
                .Where(s => s.ClienteId == clienteId)
                .OrderByDescending(s => s.DataSimulacaoUtc)
                .ToListAsync(ct);
        }
    }
}
