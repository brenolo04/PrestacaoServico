using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrestacaoServico.Models;

namespace PrestacaoServico.Data.Mapping
{
    public class OrdemServicoMap : IEntityTypeConfiguration<OrdemServico>
    {
        public void Configure(EntityTypeBuilder<OrdemServico> builder)
        {
            builder.ToTable("OrdemServico");
            builder.HasKey(os => os.Id);
            builder.Property(os => os.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(os => os.ServicoId)
                   .IsRequired();

            builder.HasOne(os => os.Servico)
                .WithMany(se => se.OrdemServicos)
                .HasForeignKey(os => os.ServicoId)
                .HasConstraintName("FK_OrdemServico_ServicoId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(os => os.SolicitanteId)
                   .IsRequired();

            builder.HasOne(os => os.Solicitante)
                .WithMany(us => us.OrdemServicos)
                .HasForeignKey(os => os.SolicitanteId)
                .HasConstraintName("FK_OrdemServico_SolicitanteId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(os => os.PrestadorId)
                   .IsRequired();

            builder.HasOne(os => os.Prestador)
                .WithMany(us => us.OrdemServicos)
                .HasForeignKey(os => os.PrestadorId)
                .HasConstraintName("FK_OrdemServico_PrestadorId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(os => os.DataSolicitacao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(os => os.Status)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
