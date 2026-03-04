using SimuladorCaixa.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Aplicacao.DTOS.Simulacoes
{
    public sealed record ItemHistoricoSimulacaoResposta(
        long SimulacaoId,
        int ProdutoId,
        string NomeProduto,
        TipoProduto TipoProduto,
        decimal RentabilidadeAnual,
        int PrazoMeses,
        decimal Valor,
        decimal ValorFinal,
        DateTime DataSimulacaoUtc
    );
}
