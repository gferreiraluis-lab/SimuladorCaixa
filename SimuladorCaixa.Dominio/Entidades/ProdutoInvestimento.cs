using SimuladorCaixa.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Dominio.Entidades
{
    public sealed class ProdutoInvestimento
    {
        public int Id { get; private set; }

        public string Nome { get; private set; } = string.Empty;

        public TipoProduto TipoProduto { get; private set; }

        /// <summary>
        /// Ex.: 0,12 significa 12% ao ano.
        /// </summary>
        public decimal RentabilidadeAnual { get; private set; }

        public NivelRisco Risco { get; private set; }

        public int PrazoMinimoMeses { get; private set; }
        public int PrazoMaximoMeses { get; private set; }

        public decimal ValorMinimo { get; private set; }
        public decimal ValorMaximo { get; private set; }

        // Construtor para EF Core
        private ProdutoInvestimento() { }

        public ProdutoInvestimento(
            string nome,
            TipoProduto tipoProduto,
            decimal rentabilidadeAnual,
            NivelRisco risco,
            int prazoMinimoMeses,
            int prazoMaximoMeses,
            decimal valorMinimo,
            decimal valorMaximo)
        {
            Nome = nome;
            TipoProduto = tipoProduto;
            RentabilidadeAnual = rentabilidadeAnual;
            Risco = risco;
            PrazoMinimoMeses = prazoMinimoMeses;
            PrazoMaximoMeses = prazoMaximoMeses;
            ValorMinimo = valorMinimo;
            ValorMaximo = valorMaximo;
        }

        public bool EhElegivel(decimal valor, int prazoMeses, TipoProduto tipoProduto)
        {
            if (tipoProduto != TipoProduto) return false;
            if (prazoMeses < PrazoMinimoMeses || prazoMeses > PrazoMaximoMeses) return false;
            if (valor < ValorMinimo || valor > ValorMaximo) return false;
            return true;
        }
    }
}
