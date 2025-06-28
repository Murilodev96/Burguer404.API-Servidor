using Burguer404.Application.Arguments.Produto;
using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Application.UseCases.Produto
{
    public sealed class AtualizarProdutoUseCase
    {
        private readonly ProdutoGateway _produtoGateway;

        public AtualizarProdutoUseCase(ProdutoGateway produtoGateway)
        {
            _produtoGateway = produtoGateway;
        }

        public static AtualizarProdutoUseCase Create(ProdutoGateway produtoGateway)
        {
            return new AtualizarProdutoUseCase(produtoGateway);
        }

        public async Task<ProdutoEntity?> ExecuteAsync(ProdutoRequest request)
        { 
            var produto = ProdutoEntity.MapProduto(request);

            if (!(produto is ProdutoEntity))
                return null;

            return await _produtoGateway.AtualizarProdutoAsync(produto);
        }
    }
}