using Burguer404.Application.Arguments.Pedido;
using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Burguer404.Domain.Ports.Repositories.Produto;
using Burguer404.Domain.Validators.Pedido;

namespace Burguer404.Domain.UseCases.Pedido
{
    public sealed class VisualizarPedidoUseCase
    {
        private readonly IRepositoryPedido _pedidoRepository;
        private readonly IRepositoryProduto _produtoRepository;

        public VisualizarPedidoUseCase(IRepositoryPedido pedidoRepository,
                                       IRepositoryProduto produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<PedidoEntity> ExecuteAsync(string codigo) =>
            await _pedidoRepository.ObterPedidoPorCodigoPedido(codigo);

        public async Task<ProdutoEntity> ObterProdutosPedido(int idProduto) =>
            await _produtoRepository.ObterProdutoPorId(idProduto);
    }
}