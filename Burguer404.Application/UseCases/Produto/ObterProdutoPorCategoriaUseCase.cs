using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Ports.Repositories.Produto;

namespace Burguer404.Application.UseCases.Produto
{
    public sealed class ObterProdutosPorCategoriaUseCase
    {
        private readonly ProdutoGateway _produtoGateway;

        public ObterProdutosPorCategoriaUseCase(ProdutoGateway produtoGateway)
        {
            _produtoGateway = produtoGateway;
        }

        public static ObterProdutosPorCategoriaUseCase Create(ProdutoGateway produtoGateway)
        {
            return new ObterProdutosPorCategoriaUseCase(produtoGateway);
        }

        public async Task<List<ProdutoEntity>?> ExecuteAsync(int categoriaId)
        {
            if (categoriaId <= 0)
                return null;
        
            return await _produtoGateway.ObterProdutosPorCategoriaIdAsync(categoriaId);
        }

    }
}