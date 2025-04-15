using Burguer404.Domain.Entities.Pedido;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Burguer404.Infrastructure.Data.Maps
{
    public class PedidoMap : IEntityTypeConfiguration<PedidoEntity>
    {
        public void Configure(EntityTypeBuilder<PedidoEntity> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.CodigoPedido).IsRequired().HasMaxLength(50);
            builder.Property(x => x.StatusPedidoId).IsRequired();

            builder.HasOne(p => p.StatusPedido)
                    .WithMany()
                    .HasForeignKey(p => p.StatusPedidoId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
