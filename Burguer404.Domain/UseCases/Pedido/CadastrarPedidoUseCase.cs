using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Burguer404.Domain.Validators.Pedido;

namespace Burguer404.Domain.UseCases.Pedido
{
    public sealed class CadastrarPedidoUseCase
    {
        private readonly IRepositoryPedido _pedidoRepository;

        public CadastrarPedidoUseCase(IRepositoryPedido pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<string> ExecuteAsync(PedidoEntity request)
        {

            var pedido = await _pedidoRepository.CriarPedido(request);


            pedido.PedidoProduto = [.. pedido.ProdutosSelecionados
                                            .GroupBy(id => id)
                                            .Select(g => new PedidoProdutoEntity
                                            {
                                                PedidoId  = pedido.Id,
                                                ProdutoId = g.Key,
                                                Quantidade = g.Count()
                                            })];

            await _pedidoRepository.InserirProdutosNoPedido([.. pedido.PedidoProduto]);
           
            return "Pedido realizado com sucesso!";
        }
    }
}