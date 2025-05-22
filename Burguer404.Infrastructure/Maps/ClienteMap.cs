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

            builder.Property(x => x.PerfilClienteId)
                   .IsRequired();

            builder.HasOne(x => x.PerfilCliente)
                    .WithMany(y => y.Cliente)
                    .HasForeignKey(x => x.PerfilClienteId)
                    .OnDelete(DeleteBehavior.Restrict);  

            builder.HasIndex(x => x.Cpf).IsUnique();

            builder.HasData(new ClienteEntity { Id = 1, Cpf = "111.111.111-11", Email = "admin@admin.com", Nome = "admin", Status = true, PerfilClienteId = 1 },
                            new ClienteEntity { Id = 2, Cpf = "123.456.789-10", Email = "usuario@usuario.com", Nome = "usuario", Status = true, PerfilClienteId = 2 });
        }
    }
}
