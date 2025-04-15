using Burguer404.Domain.Entities.Cliente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Burguer404.Infrastructure.Data.Maps
{
    public class ClienteMap : IEntityTypeConfiguration<ClienteEntity>
    {
        public void Configure(EntityTypeBuilder<ClienteEntity> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(130);

            builder.Property(x => x.Cpf)
                   .IsRequired()
                   .HasMaxLength(14)
                   .IsFixedLength();

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.HasIndex(x => x.Cpf).IsUnique();
        }
    }
}
