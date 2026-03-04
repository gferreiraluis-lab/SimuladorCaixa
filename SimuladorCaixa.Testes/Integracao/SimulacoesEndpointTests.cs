using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;


using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SimuladorCaixa.Testes.Integracao
{
    public class SimulacoesEndpointTests : IClassFixture<ApiFactory>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public SimulacoesEndpointTests(ApiFactory factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }

        private sealed class TokenResponse
        {
            public string Token { get; set; } = string.Empty;
        }


        private async Task<HttpClient> CriarClienteAutenticadoAsync()
        {
            var client = _factory.CreateClient();

            var loginResponse = await client.PostAsJsonAsync("/auth/token",
                new { usuario = "admin", senha = "admin" });

            loginResponse.EnsureSuccessStatusCode();

            var tokenObj = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>();
            tokenObj.Should().NotBeNull();
            tokenObj!.Token.Should().NotBeNullOrWhiteSpace();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenObj.Token);

            return client;
        }

        [Fact]
        public async Task Post_Simulacao_Deve_Retornar_201()
        {
            var client = await CriarClienteAutenticadoAsync();

            var request = new
            {
                clienteId = 999,
                valor = 10000m,
                prazoMeses = 12,
                tipoProduto = 1
            };

            var response = await client.PostAsJsonAsync("/simulacoes", request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Get_Simulacoes_Deve_Retornar_Lista_Do_Cliente()
        {
            var client = await CriarClienteAutenticadoAsync();

            var request = new
            {
                clienteId = 777,
                valor = 10000m,
                prazoMeses = 12,
                tipoProduto = 1
            };

            var postResponse = await client.PostAsJsonAsync("/simulacoes", request);
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created); ;

            
            var getResponse = await client.GetAsync("/simulacoes?clienteId=777");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);


            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = await getResponse.Content.ReadAsStringAsync();
            body.Should().Contain("\"simulacaoId\"");
        }

        [Fact]
        public async Task Post_Simulacao_Deve_Retornar_422_Quando_Nao_Ha_Produto_Elegivel()
        {
            var client = await CriarClienteAutenticadoAsync();

            var request = new
            {
                clienteId = 888,
                valor = 999999999m, // valor alto para estourar valorMaximo do seed
                prazoMeses = 12,
                tipoProduto = 1
            };

            var response = await client.PostAsJsonAsync("/simulacoes", request); 

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task Post_Simulacao_Sem_Token_Deve_Retornar_401()
        {
            var client = _factory.CreateClient();

            var request = new
            {
                clienteId = 123,
                valor = 1000,
                prazoMeses = 6,
                tipoProduto = 1
            };

            var response = await client.PostAsJsonAsync("/simulacoes", request);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Post_Simulacao_Com_Token_Deve_Retornar_201()
        {
            var client = _factory.CreateClient();

            var loginResponse = await client.PostAsJsonAsync("/auth/token",
                new { usuario = "admin", senha = "admin" });

            var loginContent = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", loginContent!.Token);

            var request = new
            {
                clienteId = 123,
                valor = 1000,
                prazoMeses = 6,
                tipoProduto = 1
            };

            var response = await client.PostAsJsonAsync("/simulacoes", request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }


    }
}
