using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using SimuladorCaixa.Api.Validacoes;
using SimuladorCaixa.Aplicacao.CasosDeUso.Simulacoes.CriarSimulacao;
using SimuladorCaixa.Aplicacao.CasosDeUso.Simulacoes.ListarSimulacoesPorCliente;
using SimuladorCaixa.Aplicacao.Contratos.Persistencia;
using SimuladorCaixa.Infraestrutura.Persistencia;
using SimuladorCaixa.Infraestrutura.Persistencia.Repositorios;
using SimuladorCaixa.Infraestrutura.Persistencia.Seed;
using SimuladorCaixa.Aplicacao.CasosDeUso.Simulacoes.ObterResumoSimulacoesPorCliente;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Connection string
builder.Services.AddDbContext<SimuladorCaixaDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositórios
builder.Services.AddScoped<IRepositorioProdutos, RepositorioProdutos>();
builder.Services.AddScoped<IRepositorioSimulacoes, RepositorioSimulacoes>();
builder.Services.AddScoped<ObterResumoSimulacoesPorClienteManipulador>();

// Caso de uso
builder.Services.AddScoped<CriarSimulacaoManipulador>();
builder.Services.AddScoped<ListarSimulacoesPorClienteManipulador>();

builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<CriarSimulacaoRequisicaoValidator>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Informe: Bearer {seu_token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    });
});

var jwtSection = builder.Configuration.GetSection("Jwt");
var issuer = jwtSection["Issuer"];
var audience = jwtSection["Audience"];
var key = jwtSection["Key"]!;

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
//app.UseHttpsRedirection();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var contexto = scope.ServiceProvider.GetRequiredService<SimuladorCaixaDbContext>();
    await InicializadorBanco.InicializarAsync(contexto, CancellationToken.None);
}

app.MapControllers();



app.Run();

public partial class Program { }

