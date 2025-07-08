using Burguer404.Application.Arguments.Produto;
using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Application.UseCases.Produto
{
    public class ListarProdutosUseCase
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

        public void ConverterBase64(List<ProdutoEntity> produtos)
        {
            Parallel.ForEach(produtos, produto =>
            {
                produto.ImagemBase64 = "data:image/png;base64," + Convert.ToBase64String(produto.ImagemByte ?? []);
            });
        }

        public async Task<List<ProdutoEntity>> ExecuteAsync() =>
            await _produtoGateway.ListarProdutosAsync();        
    }
}