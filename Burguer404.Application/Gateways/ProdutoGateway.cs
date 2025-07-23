using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Ports.Repositories.Produto;

using Burguer404.Domain.Interfaces.Gateways;

namespace Burguer404.Application.Gateways
{
    public class ProdutoGateway : IProdutoGateway
    {
        IRepositoryProduto _repository;

        public ProdutoGateway(IRepositoryProduto repository)
        {
            _repository = repository;
        }


        public async Task<ProdutoEntity> CadastrarProdutoAsync(ProdutoEntity produto)
            => await _repository.CadastrarProduto(produto);
        
        public async Task<List<ProdutoEntity>> ListarProdutosAsync()
            => await _repository.ListarProdutos();

        public async Task<ProdutoEntity?> AtualizarProdutoAsync(ProdutoEntity produto)
            => await _repository.AtualizarCadastro(produto);
        
        public async Task<ProdutoEntity?> ObterProdutoPorIdAsync(int produtoId)
            => await _repository.ObterProdutoPorId(produtoId);
        
        public async Task RemoverProdutoAsync(ProdutoEntity produto)
            => await _repository.RemoverProduto(produto);
        
        public async Task<List<ProdutoEntity>?> ObterProdutosPorCategoriaIdAsync(int categoriaId)
            => await _repository.ObterProdutosPorCategoriaId(categoriaId);

        public async Task<ProdutoEntity?> VisualizarImagemAsync(int produtoId)
            => await _repository.ObterProdutoPorId(produtoId);
    }
}
