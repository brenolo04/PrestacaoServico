using Microsoft.EntityFrameworkCore;
using PrestacaoServico.Data.Mapping;
using PrestacaoServico.Models;

namespace PrestacaoServico.Data
{
    public class PrestacaoServicoContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<OrdemServico> OrdemServicos { get; set; }
        public DbSet<Solicitante> Solicitantes { get; set; }
        public DbSet<Prestador> Prestadores { get; set; }
        public DbSet<Servico> Servicos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=PrestacaoServicoDB;User Id=sa;Password=fonseca@04;Encrypt=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new OrdemServicoMap()); 
            modelBuilder.ApplyConfiguration(new SolicitanteMap());
            modelBuilder.ApplyConfiguration(new PrestadorMap());
            modelBuilder.ApplyConfiguration(new ServicoMap());
        }
    }
}
