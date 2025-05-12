using Burguer404.Domain.Entities.Produto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Burguer404.Infrastructure.Data.Maps
{
    public class ProdutoMap : IEntityTypeConfiguration<ProdutoEntity>
    {
        public void Configure(EntityTypeBuilder<ProdutoEntity> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Descricao)
                   .HasMaxLength(255);

            builder.Property(x => x.Preco)
                   .IsRequired();

            builder.Property(x => x.ImagemByte)
                   .HasColumnType("varbinary(max)"); ;

            builder.Property(x => x.CategoriaPedidoId)
                   .IsRequired();

            builder.HasOne(x => x.CategoriaPedido)
                   .WithMany()
                   .HasForeignKey(x => x.CategoriaPedidoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
