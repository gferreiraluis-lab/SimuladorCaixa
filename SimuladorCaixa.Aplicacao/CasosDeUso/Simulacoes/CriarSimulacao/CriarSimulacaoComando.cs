using SimuladorCaixa.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Aplicacao.CasosDeUso.Simulacoes.CriarSimulacao
{
    public sealed record CriarSimulacaoComando(
        long ClienteId,
        decimal Valor,
        int PrazoMeses,
        TipoProduto TipoProduto
    );
}
