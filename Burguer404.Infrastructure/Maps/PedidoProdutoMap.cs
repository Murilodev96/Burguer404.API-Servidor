using Burguer404.Domain.Entities.Pedido;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burguer404.Infrastructure.Data.Maps
{
    public class PedidoProdutoMap : IEntityTypeConfiguration<PedidoProdutoEntity>
    {
        public void Configure(EntityTypeBuilder<PedidoProdutoEntity> builder)
        {
            builder.ToTable("PedidoProduto");

            builder.HasKey(pp => pp.Id); // usa o Id herdado

            builder.HasOne(pp => pp.Pedido)
                .WithMany(p => p.PedidoProduto)
                .HasForeignKey(pp => pp.PedidoId);

            builder.HasOne(pp => pp.Produto)
                .WithMany(p => p.PedidoProduto)
                .HasForeignKey(pp => pp.ProdutoId);

            builder.Property(pp => pp.Quantidade).IsRequired();
        }
    }
}
