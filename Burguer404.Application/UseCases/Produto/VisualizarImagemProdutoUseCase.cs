using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Application.UseCases.Produto
{
    public  class VisualizarImagemProdutoUseCase
    {
        private readonly ProdutoGateway _produtoGateway;

        public VisualizarImagemProdutoUseCase(ProdutoGateway produtoGateway)
        {
            _produtoGateway = produtoGateway;
        }

        public static VisualizarImagemProdutoUseCase Create(ProdutoGateway produtoGateway)
        {
            return new VisualizarImagemProdutoUseCase(produtoGateway);
        }


        public async Task<(bool, string, string)> ExecuteAsync(int produtoId)
        {
            var produto = await _produtoGateway.ObterProdutoPorIdAsync(produtoId);

            if(!(produto is ProdutoEntity))
                return (false, "Imagem não encontrada!", string.Empty);

            return (true, "Imagem encontrada com sucesso!", Convert.ToBase64String(produto.ImagemByte ?? []));
        }
    }
}