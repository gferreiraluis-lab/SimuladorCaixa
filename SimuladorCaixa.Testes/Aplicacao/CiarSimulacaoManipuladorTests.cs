using FluentAssertions;
using SimuladorCaixa.Aplicacao.CasosDeUso.Simulacoes.CriarSimulacao;
using SimuladorCaixa.Aplicacao.Erros;
using SimuladorCaixa.Dominio.Entidades;
using SimuladorCaixa.Dominio.Enums;
using SimuladorCaixa.Testes.Aplicacao.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Testes.Aplicacao
{
    public class CriarSimulacaoManipuladorTests
    {
        [Fact]
        public async Task Deve_Criar_Simulacao_Quando_Produto_Elegivel()
        {
            // Arrange
            var produtos = new List<ProdutoInvestimento>
        {
            new ProdutoInvestimento(
                "CDB Teste",
                TipoProduto.Cdb,
                0.12m,
                NivelRisco.Baixo,
                6,
                24,
                1000m,
                100000m)
        };

            var repoProdutos = new FakeRepositorioProdutos(produtos);
            var repoSimulacoes = new FakeRepositorioSimulacoes();

            var manipulador = new CriarSimulacaoManipulador(repoProdutos, repoSimulacoes);

            var comando = new CriarSimulacaoComando(
                ClienteId: 1,
                Valor: 10000m,
                PrazoMeses: 12,
                TipoProduto: TipoProduto.Cdb);

            // Act
            var resultado = await manipulador.ExecutarAsync(comando, CancellationToken.None);

            // Assert
            resultado.Should().NotBeNull();
            resultado.ValorFinal.Should().BeGreaterThan(10000m);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Nao_Ha_Produto_Elegivel()
        {
            var repoProdutos = new FakeRepositorioProdutos(new List<ProdutoInvestimento>());
            var repoSimulacoes = new FakeRepositorioSimulacoes();

            var manipulador = new CriarSimulacaoManipulador(repoProdutos, repoSimulacoes);

            var comando = new CriarSimulacaoComando(1, 10000m, 12, TipoProduto.Cdb);

            await Assert.ThrowsAsync<ExcecaoSemProdutoElegivel>(() =>
                manipulador.ExecutarAsync(comando, CancellationToken.None));
        }
    }
}
