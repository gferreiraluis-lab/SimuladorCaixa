using SimuladorCaixa.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Dominio.Entidades
{
    public sealed class SimulacaoInvestimento
    {
        public long Id { get; private set; }

        public long ClienteId { get; private set; }

        public int ProdutoId { get; private set; }
        public ProdutoInvestimento? Produto { get; private set; }

        public TipoProduto TipoProduto { get; private set; }

        public decimal Valor { get; private set; }
        public int PrazoMeses { get; private set; }

        public decimal ValorFinal { get; private set; }

        public DateTime DataSimulacaoUtc { get; private set; }

        private SimulacaoInvestimento() { }

        public SimulacaoInvestimento(
            long clienteId,
            int produtoId,
            TipoProduto tipoProduto,
            decimal valor,
            int prazoMeses,
            decimal valorFinal,
            DateTime dataSimulacaoUtc)
        {
            ClienteId = clienteId;
            ProdutoId = produtoId;
            TipoProduto = tipoProduto;
            Valor = valor;
            PrazoMeses = prazoMeses;
            ValorFinal = valorFinal;
            DataSimulacaoUtc = dataSimulacaoUtc;
        }
    }
}
