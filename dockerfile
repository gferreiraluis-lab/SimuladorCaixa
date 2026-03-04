# =========================
# BUILD
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copia apenas csproj para aproveitar cache
COPY SimuladorCaixa.Dominio/SimuladorCaixa.Dominio.csproj SimuladorCaixa.Dominio/
COPY SimuladorCaixa.Aplicacao/SimuladorCaixa.Aplicacao.csproj SimuladorCaixa.Aplicacao/
COPY SimuladorCaixa.Infraestrutura/SimuladorCaixa.Infraestrutura.csproj SimuladorCaixa.Infraestrutura/
COPY SimuladorCaixa.Api/SimuladorCaixa.Api.csproj SimuladorCaixa.Api/

RUN dotnet restore SimuladorCaixa.Api/SimuladorCaixa.Api.csproj

# agora copia o restante
COPY . .

RUN dotnet publish SimuladorCaixa.Api/SimuladorCaixa.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# =========================
# RUNTIME
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# porta padrão
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SimuladorCaixa.Api.dll"]