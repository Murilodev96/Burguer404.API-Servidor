using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Application.UseCases.Produto
{
    public class RemoverProdutoUseCase
    {
        private readonly ProdutoGateway _produtoGateway;

        public RemoverProdutoUseCase(ProdutoGateway produtoGateway)
        {
            _produtoGateway = produtoGateway;
        }

        public static RemoverProdutoUseCase Create(ProdutoGateway produtoGateway)
        {
            return new RemoverProdutoUseCase(produtoGateway);
        }

        public async Task<(bool, string)> ExecuteAsync(int produtoId)
        {
            var produto = await _produtoGateway.ObterProdutoPorIdAsync(produtoId);

            if (!(produto is ProdutoEntity))
                return (false, "Produto não encontrado!");

            await _produtoGateway.RemoverProdutoAsync(produto!);
            return (true, "Produto removido com sucesso!");
        }
    }
}