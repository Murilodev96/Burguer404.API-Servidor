using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Ports.Repositories.Produto;

namespace Burguer404.Domain.UseCases.Produto
{
    public sealed class AtualizarProdutoUseCase
    {
        private readonly IRepositoryProduto _produtoRepository;

        public AtualizarProdutoUseCase(IRepositoryProduto produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<ProdutoEntity> ExecuteAsync(ProdutoEntity request) =>
            await _produtoRepository.AtualizarCadastro(request);
    }
}