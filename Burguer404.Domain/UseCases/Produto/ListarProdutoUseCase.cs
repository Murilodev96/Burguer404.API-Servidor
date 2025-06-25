using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Ports.Repositories.Produto;

namespace Burguer404.Domain.UseCases.Produto
{
    public sealed class ListarProdutosUseCase
    {
        private readonly IRepositoryProduto _produtoRepository;

        public ListarProdutosUseCase(IRepositoryProduto produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<ProdutoEntity>> ExecuteAsync() =>
            await _produtoRepository.ListarProdutos();
    }
}