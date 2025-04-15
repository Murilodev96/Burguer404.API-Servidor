using Burguer404.Domain.Entities.ClassesEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Burguer404.Infrastructure.Data.Maps
{
    public class CategoriaPedidoMap : IEntityTypeConfiguration<CategoriaPedidoEntity>
    {
        public void Configure(EntityTypeBuilder<CategoriaPedidoEntity> builder)
        {
            builder.ToTable("CategoriaPedido");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Descricao)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasData(
                new CategoriaPedidoEntity { Id = 1, Descricao = "Lanche" },
                new CategoriaPedidoEntity { Id = 2, Descricao = "Acompanhamento" },
                new CategoriaPedidoEntity { Id = 3, Descricao = "Bebida" },
                new CategoriaPedidoEntity { Id = 4, Descricao = "Sobremesa" }
            );
        }
    }
}
