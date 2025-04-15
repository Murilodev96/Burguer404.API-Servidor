using Burguer404.Domain.Entities.ClassesEnums;
using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Entities.Produto;
using Microsoft.EntityFrameworkCore;

namespace Burguer404.Infrastructure.Data.ContextDb
{
    public class Context : DbContext
    {
        public Context() { }

        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<ClienteEntity> Clientes { get; set; }
        public DbSet<ProdutoEntity> Produtos { get; set; }
        public DbSet<PedidoEntity> Pedidos { get; set; }
        public DbSet<StatusPedidoEntity> StatusPedidos { get; set; }
        public DbSet<CategoriaPedidoEntity> CategoriaPedidos { get; set; }
        public DbSet<PedidoProdutoEntity> PedidoProduto { get; set; }
        public DbSet<PerfilClienteEntity> PerfilCliente { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasSequence<int>("CodPedidoSequence", schema: "dbo")
                        .StartsAt(1)
                        .IncrementsBy(1);
        }

    }
}
