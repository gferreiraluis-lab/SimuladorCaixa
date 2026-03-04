using Microsoft.AspNetCore.Mvc;
using SimuladorCaixa.Aplicacao.CasosDeUso.Simulacoes.CriarSimulacao;
using SimuladorCaixa.Aplicacao.DTOS.Simulacoes;
using SimuladorCaixa.Aplicacao.Erros;
using SimuladorCaixa.Aplicacao.CasosDeUso.Simulacoes.ListarSimulacoesPorCliente;
using Microsoft.AspNetCore.Authorization;
using SimuladorCaixa.Aplicacao.CasosDeUso.Simulacoes.ObterResumoSimulacoesPorCliente;

namespace SimuladorCaixa.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("simulacoes")]
    public sealed class SimulacoesController : ControllerBase
    {
        private readonly CriarSimulacaoManipulador _manipulador;
        private readonly ListarSimulacoesPorClienteManipulador _manipuladorLista;
        private readonly ObterResumoSimulacoesPorClienteManipulador _manipuladorResumo;
        private readonly ILogger<SimulacoesController> _logger;

        public SimulacoesController(
        CriarSimulacaoManipulador manipulador,
        ListarSimulacoesPorClienteManipulador manipuladorLista,
        ObterResumoSimulacoesPorClienteManipulador manipuladorResumo,
        ILogger<SimulacoesController> logger)
        {
            _manipulador = manipulador;
            _manipuladorLista = manipuladorLista;
            _manipuladorResumo = manipuladorResumo;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CriarSimulacaoResposta), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CriarSimulacao(
            [FromBody] CriarSimulacaoRequisicao requisicao,
            CancellationToken ct)
        {
            _logger.LogInformation(
            "Recebida requisicao de criacao de simulacao: clienteId {ClienteId}, tipoProduto {TipoProduto}, valor {Valor}, prazoMeses {PrazoMeses}",
            requisicao.ClienteId,
            requisicao.TipoProduto,
            requisicao.Valor,
            requisicao.PrazoMeses);

            try
            {
                var comando = new CriarSimulacaoComando(
                    requisicao.ClienteId,
                    requisicao.Valor,
                    requisicao.PrazoMeses,
                    requisicao.TipoProduto);

                var resposta = await _manipulador.ExecutarAsync(comando, ct);

                _logger.LogInformation(
                "Simulacao criada com sucesso: simulacaoId {SimulacaoId}, clienteId {ClienteId}, produtoId {ProdutoId}, valorFinal {ValorFinal}",
                resposta.SimulacaoId,
                resposta.ClienteId,
                resposta.ProdutoId,
                resposta.ValorFinal);

                return CreatedAtAction(
                    nameof(CriarSimulacao),
                    new { id = resposta.SimulacaoId },
                    resposta);
            }
            catch (ExcecaoSemProdutoElegivel ex)
            {
                _logger.LogWarning(
                "Simulacao nao elegivel: clienteId {ClienteId}, tipoProduto {TipoProduto}, valor {Valor}, prazoMeses {PrazoMeses}",
                requisicao.ClienteId,
                requisicao.TipoProduto,
                requisicao.Valor,
                requisicao.PrazoMeses);

                return UnprocessableEntity(new
                {
                    mensagem = ex.Message
                });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ItemHistoricoSimulacaoResposta>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListarPorCliente([FromQuery] long clienteId, CancellationToken ct)
        {
            _logger.LogInformation(
            "Recebida requisicao de listagem de simulacoes: clienteId {ClienteId}",
            clienteId);

            if (clienteId <= 0)
            {

                _logger.LogWarning(
                "Requisicao invalida de listagem: clienteId {ClienteId}",
                clienteId);

                return BadRequest(new { mensagem = "clienteId deve ser maior que zero." });
            }

            var consulta = new ListarSimulacoesPorClienteConsulta(clienteId);
            var resposta = await _manipuladorLista.ExecutarAsync(consulta, ct);

            _logger.LogInformation(
            "Listagem concluida: clienteId {ClienteId}, quantidade {Quantidade}",
            clienteId,
            resposta.Count);

            return Ok(resposta);
        }

        [HttpGet("agregadas")]
        [ProducesResponseType(typeof(ResumoSimulacoesClienteResposta), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterAgregadasPorCliente(
            [FromQuery] long clienteId,
            CancellationToken ct)
        {

            _logger.LogInformation(
            "Recebida requisicao de agregacao de simulacoes: clienteId {ClienteId}",
            clienteId);

            if (clienteId <= 0)
            {
                _logger.LogWarning(
                "Requisicao invalida de agregacao: clienteId {ClienteId}",
                clienteId);

                return BadRequest(new { mensagem = "clienteId deve ser maior que zero." });
            }

            var consulta = new ObterResumoSimulacoesPorClienteConsulta(clienteId);
            var resposta = await _manipuladorResumo.ExecutarAsync(consulta, ct);

            _logger.LogInformation(
            "Agregacao concluida: clienteId {ClienteId}, totalSimulacoes {TotalSimulacoes}, valorTotalInvestido {ValorTotalInvestido}, valorTotalProjetado {ValorTotalProjetado}",
            resposta.ClienteId,
            resposta.TotalSimulacoes,
            resposta.ValorTotalInvestido,
            resposta.ValorTotalProjetado);

            return Ok(resposta);
        }
    }
}
