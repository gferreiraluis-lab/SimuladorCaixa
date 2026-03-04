using Microsoft.EntityFrameworkCore;
using SimuladorCaixa.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCaixa.Infraestrutura.Persistencia
{
    public sealed class SimuladorCaixaDbContext : DbContext
    {
        public SimuladorCaixaDbContext(DbContextOptions<SimuladorCaixaDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProdutoInvestimento> ProdutosInvestimento => Set<ProdutoInvestimento>();
        public DbSet<SimulacaoInvestimento> SimulacoesInvestimento => Set<SimulacaoInvestimento>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimuladorCaixaDbContext).Assembly);
        }
    }
}
