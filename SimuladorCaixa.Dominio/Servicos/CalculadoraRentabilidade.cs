using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Dominio.Servicos
{
    public static class CalculadoraRentabilidade
    {
        
        /// Fórmula sugerida
        /// valorFinal = valor * (1 + rentabilidadeAnual/12) ^ prazoMeses
        
        public static decimal CalcularValorFinal(decimal valor, decimal rentabilidadeAnual, int prazoMeses)
        {
            if (valor <= 0) throw new ArgumentOutOfRangeException(nameof(valor));
            if (prazoMeses <= 0) throw new ArgumentOutOfRangeException(nameof(prazoMeses));
            if (rentabilidadeAnual < 0) throw new ArgumentOutOfRangeException(nameof(rentabilidadeAnual));

            double baseCalculo = 1.0 + (double)rentabilidadeAnual / 12.0;
            double fator = Math.Pow(baseCalculo, prazoMeses);

            decimal valorFinal = valor * (decimal)fator;
            return Math.Round(valorFinal, 2, MidpointRounding.AwayFromZero);
        }
    }
}
