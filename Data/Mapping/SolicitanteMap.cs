using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrestacaoServico.Models;

namespace PrestacaoServico.Data.Mapping
{
    public class SolicitanteMap : IEntityTypeConfiguration<Solicitante>
    {
        public void Configure(EntityTypeBuilder<Solicitante> builder)
        {
            builder.ToTable("Solicitante");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(s => s.Nome)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.HasOne(s => s.Usuario)
                .WithMany()
                .HasForeignKey(s => s.UsuarioId);
        }
    }
}
