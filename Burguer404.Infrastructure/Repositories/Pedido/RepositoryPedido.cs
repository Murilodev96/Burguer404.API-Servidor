using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Enums;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Burguer404.Infrastructure.Data.ContextDb;
using Microsoft.EntityFrameworkCore;

namespace Burguer404.Infrastructure.Data.Repositories.Pedido
{
    public class RepositoryPedido : IRepositoryPedido
    {
        private readonly Context _context;

        public RepositoryPedido(Context context)
        {
            _context = context;
        }

        public async Task<PedidoEntity> CriarPedido(PedidoEntity pedido)
        {
            pedido.CodigoPedido = GerarCodigoPedido();
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public string GerarCodigoPedido()
        {
            var valor = _context.Database
                        .SqlQuery<long>($"SELECT NEXT VALUE FOR dbo.CodPedidoSequence")
                        .AsEnumerable()
                        .First();

            return $"PED-{valor}";
        }

        public async Task InserirProdutosNoPedido(List<PedidoProdutoEntity> pedidoProdutos)
        {
            _context.PedidoProduto.AddRange(pedidoProdutos);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PedidoEntity>> ListarPedidos()
            => await _context.Pedidos.Include(p => p.StatusPedido)
                                     .Include(p => p.Cliente)
                                     .Include(p => p.PedidoProduto)
                                     .ToListAsync();

        public async Task<bool> CancelarPedido(int pedidoId)
        {
            var pedido = await _context.Pedidos.FindAsync(pedidoId);

            if (pedido != null)
                return false;

            pedido!.StatusPedidoId = (int)EnumStatusPedido.Cancelado;
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PedidoEntity?> ObterPedidoPorCodigoPedido(string codigo)
        {
            var pedido = await _context.Pedidos.Where(p => p.CodigoPedido == codigo)
                                     .Include(p => p.StatusPedido)
                                     .Include(p => p.Cliente)
                                     .Include(p => p.PedidoProduto)
                                     .FirstOrDefaultAsync();

            return pedido;
        }

        public async Task<bool> AlterarStatusPedido(PedidoEntity pedido)
        {
            var pedidoEntity = await _context.Pedidos.FindAsync(pedido.Id);

            if (pedido == null)
                return false;

            pedido!.StatusPedidoId = pedido.StatusPedidoId;
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
