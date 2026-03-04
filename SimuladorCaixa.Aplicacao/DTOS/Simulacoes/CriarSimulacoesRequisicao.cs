using SimuladorCaixa.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Aplicacao.DTOS.Simulacoes
{
    public sealed record CriarSimulacaoRequisicao(
        long ClienteId,
        decimal Valor,
        int PrazoMeses,
        TipoProduto TipoProduto
    );
}
