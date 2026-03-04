using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SimuladorCaixa.Aplicacao.CasosDeUso.Simulacoes.CriarSimulacao;
using SimuladorCaixa.Aplicacao.DTOS.Simulacoes;
using SimuladorCaixa.Aplicacao.Erros;

namespace SimuladorCaixa.Api.Validacoes
{
    public sealed class CriarSimulacaoRequisicaoValidator
     : AbstractValidator<CriarSimulacaoRequisicao>
    {
        public CriarSimulacaoRequisicaoValidator()
        {
            RuleFor(x => x.ClienteId)
                .GreaterThan(0);

            RuleFor(x => x.Valor)
                .GreaterThan(0);

            RuleFor(x => x.PrazoMeses)
                .GreaterThan(0);

            RuleFor(x => x.TipoProduto)
                .IsInEnum();
        }
    }
}
