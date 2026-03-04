using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimuladorCaixa.Infraestrutura.Persistencia;


using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Testes.Integracao
{
    public sealed class ApiFactory : WebApplicationFactory<Program>
    {
        private SqliteConnection? _conexao;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove o DbContext registrado anteriormente
                var descriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<SimuladorCaixaDbContext>));

                if (descriptor is not null)
                    services.Remove(descriptor);

                // Cria conexão SQLite em memória (mantida aberta durante o teste)
                _conexao = new SqliteConnection("Data Source=:memory:");
                _conexao.Open();

                services.AddDbContext<SimuladorCaixaDbContext>(options =>
                {
                    options.UseSqlite(_conexao);
                });

                // Cria o banco e aplica migrations
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<SimuladorCaixaDbContext>();
                db.Database.Migrate();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _conexao?.Dispose();
        }
    }
}
