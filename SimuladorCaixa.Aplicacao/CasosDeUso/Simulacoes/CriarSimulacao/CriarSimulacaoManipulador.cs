using SimuladorCaixa.Aplicacao.Contratos.Persistencia;
using SimuladorCaixa.Aplicacao.DTOS.Simulacoes;
using SimuladorCaixa.Aplicacao.Erros;
using SimuladorCaixa.Dominio.Entidades;
using SimuladorCaixa.Dominio.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Aplicacao.CasosDeUso.Simulacoes.CriarSimulacao
{
    public sealed class CriarSimulacaoManipulador
    {
        private readonly IRepositorioProdutos _repositorioProdutos;
        private readonly IRepositorioSimulacoes _repositorioSimulacoes;


        public CriarSimulacaoManipulador(
            IRepositorioProdutos repositorioProdutos,
            IRepositorioSimulacoes repositorioSimulacoes)
        {
            _repositorioProdutos = repositorioProdutos;
            _repositorioSimulacoes = repositorioSimulacoes;
        }

        public async Task<CriarSimulacaoResposta> ExecutarAsync(CriarSimulacaoComando comando, CancellationToken ct)
        {
            // 1) Buscar produtos do tipo
            var produtos = await _repositorioProdutos.ListarPorTipoAsync(comando.TipoProduto, ct);

            // 2) Filtrar elegíveis
            var produtoEscolhido = produtos
                .Where(p => p.EhElegivel(comando.Valor, comando.PrazoMeses, comando.TipoProduto))
                .OrderByDescending(p => p.RentabilidadeAnual) // regra simples e defensável
                .FirstOrDefault();

            if (produtoEscolhido is null)
                throw new ExcecaoSemProdutoElegivel();

            // 3) Calcular valor final
            var valorFinal = CalculadoraRentabilidade.CalcularValorFinal(
                comando.Valor,
                produtoEscolhido.RentabilidadeAnual,
                comando.PrazoMeses);

            // 4) Criar entidade Simulacao
            var agoraUtc = DateTime.UtcNow;

            var simulacao = new SimulacaoInvestimento(
                clienteId: comando.ClienteId,
                produtoId: produtoEscolhido.Id,
                tipoProduto: comando.TipoProduto,
                valor: comando.Valor,
                prazoMeses: comando.PrazoMeses,
                valorFinal: valorFinal,
                dataSimulacaoUtc: agoraUtc);

            // 5) Persistir
            await _repositorioSimulacoes.AdicionarAsync(simulacao, ct);

            // 6) Retornar DTO
            return new CriarSimulacaoResposta(
                SimulacaoId: simulacao.Id,
                ClienteId: simulacao.ClienteId,
                ProdutoId: produtoEscolhido.Id,
                NomeProduto: produtoEscolhido.Nome,
                TipoProduto: produtoEscolhido.TipoProduto,
                RentabilidadeAnual: produtoEscolhido.RentabilidadeAnual,
                PrazoMeses: simulacao.PrazoMeses,
                Valor: simulacao.Valor,
                ValorFinal: simulacao.ValorFinal,
                DataSimulacaoUtc: simulacao.DataSimulacaoUtc
            );
        }
    }
}
