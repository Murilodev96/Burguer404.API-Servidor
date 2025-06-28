using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Application.UseCases.Produto
{
    public sealed class ListarProdutosUseCase
    {
        private readonly ProdutoGateway _produtoGateway;

        public ListarProdutosUseCase(ProdutoGateway produtoGateway)
        {
            _produtoGateway = produtoGateway;
        }

        public static ListarProdutosUseCase Create(ProdutoGateway produtoGateway)
        {
            return new ListarProdutosUseCase(produtoGateway);
        }

        public async Task<List<ProdutoEntity>> ExecuteAsync() =>
            await _produtoGateway.ListarProdutosAsync();
    }
}