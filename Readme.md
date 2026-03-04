# SimuladorCaixa

## Pré-requisitos

- [.NET 9](https://dotnet.microsoft.com/download)

- [Docker](https://www.docker.com/)

## Esquema de Arquitetura

![alt text](diagrama.png)

## Tomadas de decisões e Escolhas

Clean Architecture

O projeto foi estruturado utilizando Clean Architecture para garantir:

- separação de responsabilidades

- baixo acoplamento entre camadas

- facilidade de testes

- independência da infraestrutura


JWT

Foi implementada autenticação utilizando JWT Bearer Token.

Fluxo:

- Usuário obtém token via endpoint /auth/token

- Token é utilizado para acessar endpoints protegidos.

Logs Estruturados

O projeto implementa logs estruturados utilizando ILogger, permitindo rastreamento de eventos importantes como:

- autenticação

- criação de simulação

- consultas

- regras de negócio

Endpoint de Agregação 

Foi implementado um endpoint para consolidar informações das simulações de um cliente.

GET /simulacoes/agregadas?clienteId=123

Retorna:

- total investido

- total projetado

- ganho total

- quantidade de simulações

- rentabilidade média


## Como executar em ambiente local

- Acesse a pasta do projeto
cd SimuladorCaixa
- Restaurar pacotes comando: 
dotnet restore
- Executar a API comando: 
dotnet run --project SimuladorCaixa.Api

- Acessar documentação da API

Após iniciar a aplicação, acessar:

http://localhost:"porta"/swagger

## Autenticação

Para acessar os endpoints protegidos, primeiro gere um token.

Endpoint
POST /auth/token
Body
{
  "usuario": "admin",
  "senha": "admin"
}

## Utilização

No Swagger clique em Authorize e informe:

Bearer <seu_token>
Testes

## Para rodar os testes automatizados:

dotnet test

Os testes incluem:

- testes unitários

- testes de integração dos endpoints

## Executar com Docker
- Build da imagem comando: 
docker build -t simuladorcaixa-api .
- Executar container comando: 
docker run --rm -p 8080:8080 simuladorcaixa-api

Swagger disponível em:

"http://localhost:8080/swagger"




