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

            builder.Property(x => x.CategoriaProdutoId)
                   .IsRequired();

            builder.HasOne(x => x.CategoriaProduto)
                   .WithMany()
                   .HasForeignKey(x => x.CategoriaProdutoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new ProdutoEntity { Id = 1, Nome = "X-Bacon", Descricao = "adicional de bacon", Preco = 31.99, CategoriaProdutoId = 1, ImagemByte = null },
                new ProdutoEntity { Id = 2, Nome = "Coca-Cola", Descricao = "Zero açucar", Preco = 7.0, CategoriaProdutoId = 3, ImagemByte = null },
                new ProdutoEntity { Id = 3, Nome = "Batata frita", Descricao = "300g", Preco = 15.0, CategoriaProdutoId = 2, ImagemByte = null },
                new ProdutoEntity { Id = 4, Nome = "Sorvete", Descricao = "Morango", Preco = 9.0, CategoriaProdutoId = 4, ImagemByte = null },
                new ProdutoEntity { Id = 5, Nome = "X-Salada", Descricao = "saladinha da boa", Preco = 24.99, CategoriaProdutoId = 1, ImagemByte = null },
                new ProdutoEntity { Id = 6, Nome = "Pepsi", Descricao = "concorrente", Preco = 7.0, CategoriaProdutoId = 3, ImagemByte = null },
                new ProdutoEntity { Id = 7, Nome = "Onion rings", Descricao = "300g", Preco = 20.0, CategoriaProdutoId = 2, ImagemByte = null },
                new ProdutoEntity { Id = 8, Nome = "Bolo de pote", Descricao = "Chocolate com morango", Preco = 14.0, CategoriaProdutoId = 4, ImagemByte = null },
                new ProdutoEntity { Id = 9, Nome = "X-Tudo", Descricao = "tudo do bom e do melhor", Preco = 40.0, CategoriaProdutoId = 1, ImagemByte = null },
                new ProdutoEntity { Id = 10, Nome = "Suco de maracuja", Descricao = "suquinho", Preco = 10.0, CategoriaProdutoId = 3, ImagemByte = null },
                new ProdutoEntity { Id = 11, Nome = "Batata + Onion rings P", Descricao = "400g", Preco = 27.5, CategoriaProdutoId = 2, ImagemByte = null },
                new ProdutoEntity { Id = 12, Nome = "Pudim", Descricao = "Melhor de todos", Preco = 99.0, CategoriaProdutoId = 4, ImagemByte = null },
                new ProdutoEntity { Id = 13, Nome = "X-Frango", Descricao = "fitness", Preco = 22.99, CategoriaProdutoId = 1, ImagemByte = null },
                new ProdutoEntity { Id = 14, Nome = "X-Calabresa", Descricao = "pouca gordura graças a Deus", Preco = 26.99, CategoriaProdutoId = 1, ImagemByte = null },
                new ProdutoEntity { Id = 15, Nome = "X-Picanha", Descricao = "suculência ao máximo", Preco = 36.99, CategoriaProdutoId = 1, ImagemByte = null },
                new ProdutoEntity { Id = 16, Nome = "Suco de limão", Descricao = "suquinho 2", Preco = 7.0, CategoriaProdutoId = 3, ImagemByte = null },
                new ProdutoEntity { Id = 17, Nome = "H2O", Descricao = "água de torneira", Preco = 5.0, CategoriaProdutoId = 3, ImagemByte = null },
                new ProdutoEntity { Id = 18, Nome = "Batata + Onion rings M", Descricao = "700g", Preco = 33.0, CategoriaProdutoId = 2, ImagemByte = null },
                new ProdutoEntity { Id = 19, Nome = "Batata + Onion rings G", Descricao = "1Kg", Preco = 41.0, CategoriaProdutoId = 2, ImagemByte = null }
                );
        }
    }
}
