using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Ports.Repositories.Produto;

namespace Burguer404.Domain.UseCases.Produto
{
    public sealed class RemoverProdutoUseCase
    {
        private readonly IRepositoryProduto _produtoRepository;

        public RemoverProdutoUseCase(IRepositoryProduto produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task ExecuteAsync(ProdutoEntity produto) =>
            await _produtoRepository.RemoverProduto(produto!);
    }
}