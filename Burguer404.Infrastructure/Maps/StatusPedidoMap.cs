using Burguer404.Domain.Entities.ClassesEnums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Burguer404.Infrastructure.Data.Maps
{
    public class StatusPedidoMap : IEntityTypeConfiguration<StatusPedidoEntity>
    {
        public void Configure(EntityTypeBuilder<StatusPedidoEntity> builder)
        {
            builder.ToTable("StatusPedido");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Descricao)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasData(
                new StatusPedidoEntity { Id = 1, Descricao = "Recebido" },
                new StatusPedidoEntity { Id = 2, Descricao = "Em preparação" },
                new StatusPedidoEntity { Id = 3, Descricao = "Pronto" },
                new StatusPedidoEntity { Id = 4, Descricao = "Finalizado" },
                new StatusPedidoEntity { Id = 5, Descricao = "Cancelado" }
            );
        }
    }
}
