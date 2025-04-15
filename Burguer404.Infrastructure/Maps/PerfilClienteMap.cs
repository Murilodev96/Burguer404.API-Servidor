using Burguer404.Domain.Entities.Cliente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Burguer404.Infrastructure.Data.Maps
{
    public class PerfilClienteMap : IEntityTypeConfiguration<PerfilClienteEntity>
    {
        public void Configure(EntityTypeBuilder<PerfilClienteEntity> builder)
        {
            builder.ToTable("PerfilCliente");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Descricao)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(pp => pp.Cliente)
                .WithOne(p => p.PerfilCliente)
                .HasForeignKey(pp => pp.PerfilClienteId);

            builder.HasData(
                    new PerfilClienteEntity {Id = 1, Descricao = "admin"},
                    new PerfilClienteEntity { Id = 2, Descricao = "usuario" }
                );

        }
    }
}
