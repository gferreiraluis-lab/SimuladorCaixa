using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Aplicacao.DTOS.Simulacoes
{
    public sealed record ResumoSimulacoesClienteResposta(
    long ClienteId,
    int TotalSimulacoes,
    decimal ValorTotalInvestido,
    decimal ValorTotalProjetado,
    decimal GanhoTotal,
    decimal RentabilidadeMediaPercentual
    );
}
