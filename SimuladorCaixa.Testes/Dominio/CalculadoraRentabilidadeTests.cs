using FluentAssertions;
using SimuladorCaixa.Dominio.Servicos;
using Xunit;

using SimuladorCaixa.Dominio.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Testes.Dominio
{
    public class CalculadoraRentabilidadeTests
    {
        [Fact]
        public void Deve_Calcular_ValorFinal_Corretamente()
        {
            
            decimal valorInicial = 10000m;
            decimal rentabilidadeAnual = 0.12m; // 12%
            int prazoMeses = 12;

            
            var resultado = CalculadoraRentabilidade
                .CalcularValorFinal(valorInicial, rentabilidadeAnual, prazoMeses);

            
            resultado.Should().BeGreaterThan(valorInicial);
            resultado.Should().BeApproximately(11268.25m, 0.01m);
        }
    }
}
