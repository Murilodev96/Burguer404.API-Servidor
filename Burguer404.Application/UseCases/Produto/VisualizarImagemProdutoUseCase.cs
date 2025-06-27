using Burguer404.Domain.Ports.Repositories.Produto;

namespace Burguer404.Application.UseCases.Produto
{
    public sealed class VisualizarImagemProdutoUseCase
    {
        private readonly IRepositoryProduto _produtoRepository;

        public VisualizarImagemProdutoUseCase(IRepositoryProduto produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<string> ExecuteAsync(int produtoId)
        {
            var produto = await _produtoRepository.VisualizarImagem(produtoId);
            return Convert.ToBase64String(produto.ImagemByte);
        }
    }
}