using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Application.Ports.Gateways
{
    public interface IProdutoGateway
    {
        Task<ProdutoEntity> CadastrarProdutoAsync(ProdutoEntity produto);
        Task<List<ProdutoEntity>> ListarProdutosAsync();
        Task<ProdutoEntity?> AtualizarProdutoAsync(ProdutoEntity produto);
        Task<ProdutoEntity?> ObterProdutoPorIdAsync(int produtoId);
        Task RemoverProdutoAsync(ProdutoEntity produto);
        Task<List<ProdutoEntity>?> ObterProdutosPorCategoriaIdAsync(int categoriaId);
    }
}