using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrestacaoServico.Models;

namespace PrestacaoServico.Data.Mapping
{
    public class ServicoMap : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Nome)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(s => s.Descricao)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(1000);

            builder.Property(s => s.Valor)
                .HasDefaultValue(0.1)
                .HasColumnType("decimal(10,2)");

        }
    }
}
