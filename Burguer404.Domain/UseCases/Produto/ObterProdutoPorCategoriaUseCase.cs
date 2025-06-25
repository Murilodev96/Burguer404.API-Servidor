using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Ports.Repositories.Produto;

namespace Burguer404.Domain.UseCases.Produto
{
    public sealed class ObterProdutosPorCategoriaUseCase
    {
        private readonly IRepositoryProduto _produtoRepository;

        public ObterProdutosPorCategoriaUseCase(IRepositoryProduto produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<ProdutoEntity>> ExecuteAsync(int categoriaId) =>
            await _produtoRepository.ObterProdutosPorCategoriaId(categoriaId);

    }
}