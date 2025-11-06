using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrestacaoServico.Models;

namespace PrestacaoServico.Data.Mapping
{
    public class PrestadorMap : IEntityTypeConfiguration<Prestador>
    {
        public void Configure(EntityTypeBuilder<Prestador> builder)
        {
            builder.ToTable("Prestador");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(p => p.Nome)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(p => p.Profissao)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.HasMany(p => p.Servicos)
                .WithOne()
                .HasForeignKey(s => s.PrestadorId);

            builder.HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId);
        }
    }
}
